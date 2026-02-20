using System;
using PayrollEngine.Application.Services;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Application.Calculators;

public class EmployeeSSCAmountCalc
{
    private readonly ActiveSSParamsService _activeSSParamsService;
    private readonly RetiredSSParamsService _retiredSSParamsService;
    private readonly EmployeeScenariosService _employeeScenariosService;

    public EmployeeSSCAmountCalc(ActiveSSParamsService activeSSParamsService, 
                                    RetiredSSParamsService retiredSSParamsService,
                                    EmployeeScenariosService employeeScenariosService)
    {   

    if (activeSSParamsService == null)
    {
        throw new ArgumentNullException(nameof(activeSSParamsService), "ActiveSSParamsService cannot be null.");
    }
    if (retiredSSParamsService == null)
    {
        throw new ArgumentNullException(nameof(retiredSSParamsService), "RetiredSSParamsService cannot be null.");
    } 
    if(employeeScenariosService == null)
    {       
         throw new ArgumentNullException(nameof(employeeScenariosService), "EmployeeScenariosService cannot be null.");
    }

        _activeSSParamsService = activeSSParamsService;
        _retiredSSParamsService = retiredSSParamsService;
        _employeeScenariosService = employeeScenariosService;
    }


    public async  Task<decimal> Calc(decimal SSContributionBase)
    {

        if (SSContributionBase < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(SSContributionBase), "SSContributionBase cannot be negative.");
        }

        
        var scenario = await _employeeScenariosService.GetAsync();
        if (scenario == null)
        {
            throw new InvalidOperationException("Employee scenario not found.");
        }


        if(scenario.Status == Status.Active)
        {   

            var activeSSParams = await _activeSSParamsService.GetValueAsync(scenario.Year);
            if (activeSSParams == null)
            {
                throw new InvalidOperationException("Active SS parameters not found for the given year.");
            }
            return SSContributionBase * activeSSParams.EmployeeSSRate;

        }
        else if(scenario.Status == Status.Retired)
        {   

            var retiredSSParams = await _retiredSSParamsService.GetValueAsync(scenario.Year);
            if (retiredSSParams == null)
            {
                throw new InvalidOperationException("Retired SS parameters not found for the given year.");
            }
            return SSContributionBase * retiredSSParams.EmployeeSSRate;
            
        }
        else
        {
            throw new InvalidOperationException("Invalid employee status.");
        }


        


    }




}
