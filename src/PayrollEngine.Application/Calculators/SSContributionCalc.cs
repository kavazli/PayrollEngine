using System;
using System.Threading.Tasks;
using PayrollEngine.Application.Services;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Interfaces.Services;

namespace PayrollEngine.Application.Calculators;

public class SSContributionCalc
{
    private readonly SSCeilingService _ssCeilingService;
    private readonly IEmployeeScenariosService _employeeScenariosService;
    private readonly MinimumWageService _minimumWageService;

    public SSContributionCalc(SSCeilingService ssCeilingService, IEmployeeScenariosService employeeScenariosService, MinimumWageService minimumWageService)
    {   
        if (ssCeilingService == null)
        {
            throw new ArgumentNullException(nameof(ssCeilingService), "SSCeilingService cannot be null.");
        }

        if (employeeScenariosService == null)
        {
            throw new ArgumentNullException(nameof(employeeScenariosService), "EmployeeScenariosService cannot be null.");
        }

        if(minimumWageService == null)
        {
            throw new ArgumentNullException(nameof(minimumWageService), "MinimumWageService cannot be null.");
        }
        
        _minimumWageService = minimumWageService;
        _ssCeilingService = ssCeilingService;
        _employeeScenariosService = employeeScenariosService;
    }


    public async Task<decimal> Calc(decimal GrossSalary)
    {   

        if(GrossSalary < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(GrossSalary), "Gross salary cannot be negative.");
        }
        
        var scenario = await _employeeScenariosService.GetAsync();
        if (scenario == null)
        {
            throw new InvalidOperationException($"Employee scenario not found.");
        }

        var ssCeiling = await _ssCeilingService.GetValueAsync(scenario.Year);   
        if (ssCeiling == null)
        {
            throw new InvalidOperationException($"SS ceiling not found for year: {scenario.Year}");
        }

        var minimumWage = await _minimumWageService.GetValueAsync(scenario.Year);
        if (minimumWage == null)
        {
            throw new InvalidOperationException($"Minimum wage not found for year: {scenario.Year}");
        }


        if (GrossSalary > ssCeiling.Ceiling)
        {
            return ssCeiling.Ceiling;
        }
        else if (GrossSalary < minimumWage.GrossAmount)
        {
            return minimumWage.GrossAmount;
        }
        else
        {
            return GrossSalary;
        }
        
       
    }

}
