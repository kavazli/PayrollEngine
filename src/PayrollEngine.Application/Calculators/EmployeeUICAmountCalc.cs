using System;
using PayrollEngine.Application.Services;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces.Services;

namespace PayrollEngine.Application.Calculators;

public class EmployeeUICAmountCalc
{
    private readonly ActiveSSParamsService _activeSSParamsService;

    private readonly IEmployeeScenariosService _employeeScenariosService;

    public EmployeeUICAmountCalc(ActiveSSParamsService activeSSParamsService, IEmployeeScenariosService employeeScenariosService)
    {
        if (activeSSParamsService == null)
        {
            throw new ArgumentNullException(nameof(activeSSParamsService), "ActiveSSParamsService cannot be null.");
        }
        if (employeeScenariosService == null)
        {
            throw new ArgumentNullException(nameof(employeeScenariosService), "EmployeeScenariosService cannot be null.");
        }

        _activeSSParamsService = activeSSParamsService;
        _employeeScenariosService = employeeScenariosService;
    }


    public async Task<Decimal> Calc(decimal SSContributionBase)
    {

        if(SSContributionBase < 0)
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
            decimal result;
            if (activeSSParams == null)
            {
                throw new InvalidOperationException($"Active SS parameters not found for year {scenario.Year}.");
            }
            result = SSContributionBase * activeSSParams.EmployeeUIRate;
            return Math.Round(result, 2);
        }
        else
        {
            return 0m; 
        }

    }


}
