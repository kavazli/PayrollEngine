using System;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces.Services;

namespace PayrollEngine.Application.Calculators;

public class EmployerContributionsCalc
{
    private readonly IEmployeeScenariosService _employeeScenariosService;
    private readonly IResultPayrollService _resultPayrollService;
    private readonly IEmployerContributionsService _employerContributionsService;
    private readonly MinimumWageService _minimumWageService;
    private readonly ActiveSSParamsService _activeSSParamsService;
    private readonly RetiredSSParamsService _retiredSSParamsService;


    public EmployerContributionsCalc(IEmployeeScenariosService employeeScenariosService,
                                      IResultPayrollService resultPayrollService,
                                      IEmployerContributionsService employerContributionsService,
                                      MinimumWageService minimumWageService,
                                      ActiveSSParamsService activeSSParamsService,
                                      RetiredSSParamsService retiredSSParamsService)
    {   

        if (employeeScenariosService == null)
        {
            throw new ArgumentNullException(nameof(employeeScenariosService), "EmployeeScenariosService cannot be null.");
        }
        if (resultPayrollService == null)        {
            throw new ArgumentNullException(nameof(resultPayrollService), "ResultPayrollService cannot be null.");
        }
        if (employerContributionsService == null)        {
            throw new ArgumentNullException(nameof(employerContributionsService), "EmployerContributionsService cannot be null.");
        }
        if (minimumWageService == null)        {
            throw new ArgumentNullException(nameof(minimumWageService), "MinimumWageService cannot be null.");
        }
        if (activeSSParamsService == null)        {
            throw new ArgumentNullException(nameof(activeSSParamsService), "ActiveSSParamsService cannot be null.");
        }
        if (retiredSSParamsService == null)        {
            throw new ArgumentNullException(nameof(retiredSSParamsService), "RetiredSSParamsService cannot be null.");
        }       


        _employeeScenariosService = employeeScenariosService;
        _resultPayrollService = resultPayrollService;
        _employerContributionsService = employerContributionsService;
        _minimumWageService = minimumWageService;
        _activeSSParamsService = activeSSParamsService;
        _retiredSSParamsService = retiredSSParamsService;
    }

    public async Task<EmployerContributions> Calc(Months months)
    {
        var scenario = await _employeeScenariosService.GetAsync();
        var resultPayroll = await _resultPayrollService.GetMonthAsync(months);
        var activeSSParams = await _activeSSParamsService.GetValueAsync(scenario.Year);
        var retiredSSParams = await _retiredSSParamsService.GetValueAsync(scenario.Year);
        var minimumWage = await _minimumWageService.GetValueAsync(scenario.Year);
        
        Sector sector = scenario.Sector;
        IncentiveType incentiveType = scenario.IncentiveType;
        Status status = scenario.Status;

        decimal totalGross = resultPayroll.SSContributionBase;
        decimal employerSSRate = activeSSParams.EmployerSSRate;
        decimal employerUIRate = activeSSParams.EmployerUIRate;
        decimal employerRetiredSSRate = retiredSSParams.EmployerSSRate;
        decimal minimumWageAmount = minimumWage.GrossAmount;

        EmployerContributions employerContributions = new EmployerContributions();



        
        // Emeklilerde sector veya İncentiveType'ye bakılmaksızın SGDP uygulanır.
        if(status == Status.Retired)
        {   
            employerContributions.Month = months;
            employerContributions.EmployerSSContributionAmount = SgdpCalc(totalGross, employerRetiredSSRate);
            employerContributions.EmployerUIContributionAmount = 0;
            employerContributions.TotalEmployerCost = totalGross + employerContributions.EmployerSSContributionAmount + employerContributions.EmployerUIContributionAmount;
            return employerContributions;
        }
       
        
        if(status == Status.Active && incentiveType == IncentiveType.None)
        {
            employerContributions.Month = months;
            employerContributions.EmployerSSContributionAmount = Full(totalGross, employerSSRate);
            employerContributions.EmployerUIContributionAmount = _UICalc(totalGross, employerUIRate);
        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code5510 && sector == Sector.Manufacturing)
        {
            employerContributions.Month = months;
            employerContributions.EmployerSSContributionAmount = _5510ManCalc(totalGross, employerSSRate);
            employerContributions.EmployerUIContributionAmount = _UICalc(totalGross, employerUIRate);
        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code5510 && sector == Sector.Other)
        {
            employerContributions.Month = months;
            employerContributions.EmployerSSContributionAmount = _5510OtherCalc(totalGross, employerSSRate);
            employerContributions.EmployerUIContributionAmount = _UICalc(totalGross, employerUIRate);
        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code14857 && sector == Sector.Manufacturing)
        {
            employerContributions.Month = months;
            employerContributions.EmployerSSContributionAmount = _14857ManCalc(totalGross, employerSSRate, minimumWageAmount);
            employerContributions.EmployerUIContributionAmount = _UICalc(totalGross, employerUIRate);
        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code14857 && sector == Sector.Other)
        {
            employerContributions.Month = months;
            employerContributions.EmployerSSContributionAmount = _14857OtherCalc(totalGross, employerSSRate, minimumWageAmount);
            employerContributions.EmployerUIContributionAmount = _UICalc(totalGross, employerUIRate);
        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code6111)
        {
            employerContributions.Month = months;
            employerContributions.EmployerSSContributionAmount = _6111Calc();
            employerContributions.EmployerUIContributionAmount = _UICalc(totalGross, employerUIRate);
        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code16322 && sector == Sector.Manufacturing)
        {
            employerContributions.Month = months;
            employerContributions.EmployerSSContributionAmount = _16322ManCalc(totalGross, employerSSRate, minimumWageAmount);
            employerContributions.EmployerUIContributionAmount = _UICalc(totalGross, employerUIRate);
        }
        else if(status == Status.Active && incentiveType == IncentiveType.Code16322 && sector == Sector.Other)
        {
            employerContributions.Month = months;
            employerContributions.EmployerSSContributionAmount = _16322OtherCalc(totalGross, employerSSRate, minimumWageAmount);
            employerContributions.EmployerUIContributionAmount = _UICalc(totalGross, employerUIRate);
        }
        else
        {
            throw new InvalidOperationException("Invalid combination of Status, IncentiveType, and Sector.");
        }

        employerContributions.TotalEmployerCost = totalGross + employerContributions.EmployerSSContributionAmount + employerContributions.EmployerUIContributionAmount;
        return employerContributions;

        
    }


    private decimal SgdpCalc(decimal totalGross, decimal employerRetiredSSRate)
    {
        return totalGross * employerRetiredSSRate;
    }

    private decimal Full(decimal totalGross, decimal employerSSRate)
    {
        return totalGross * employerSSRate;
    }

    private decimal _5510ManCalc(decimal totalGross, decimal employerSSRate)
    {
        decimal rate = employerSSRate - 0.05m;
        return totalGross * rate;
    }

    private decimal _5510OtherCalc(decimal totalGross, decimal employerSSRate)
    {
        decimal rate = employerSSRate - 0.02m;
        return totalGross * rate;
    }

    private decimal _14857ManCalc(decimal totalGross, decimal employerSSRate, decimal minimumWage)
    {
        decimal rate = employerSSRate - 0.05m;
        return (totalGross * rate) - (minimumWage * rate);
    }

    private decimal _14857OtherCalc(decimal totalGross, decimal employerSSRate, decimal minimumWage)
    {
        decimal rate = employerSSRate - 0.02m;
        return (totalGross * rate) - (minimumWage * rate);
    }

    private decimal _6111Calc()
    {
        return 0;
    }

    private decimal _16322ManCalc(decimal totalGross, decimal employerSSRate, decimal minimumWage)
    {
        decimal rate = employerSSRate - 0.05m;
        return (totalGross * rate) - (minimumWage * rate);
    }

    private decimal _16322OtherCalc(decimal totalGross, decimal employerSSRate, decimal minimumWage)
    {
        decimal rate = employerSSRate - 0.02m;
        return (totalGross * rate) - (minimumWage * rate);
    }

    private decimal _UICalc(decimal totalGross, decimal employerUIRate)
    {
        return totalGross * employerUIRate;
    }

}
