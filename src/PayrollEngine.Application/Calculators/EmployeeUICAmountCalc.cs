using System;
using PayrollEngine.Application.Services;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Application.Calculators;

public class EmployeeUICAmountCalc
{
    private readonly ActiveSSParamsService _activeSSParamsService;

    private readonly EmployeeScenariosService _employeeScenariosService;

    public EmployeeUICAmountCalc(ActiveSSParamsService activeSSParamsService, EmployeeScenariosService employeeScenariosService)
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
            if (activeSSParams == null)
            {
                throw new InvalidOperationException($"Active SS parameters not found for year {scenario.Year}.");
            }

            return SSContributionBase * activeSSParams.EmployeeUIRate;
        }
        else
        {
            return 0m; 
        }

    }


}
