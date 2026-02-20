using System;
using System.Threading.Tasks;
using PayrollEngine.Application.Services;
using PayrollEngine.Application.Services.Params;

namespace PayrollEngine.Application.Calculators;

public class SSContributionCalc
{
    private readonly SSCeilingService _ssCeilingService;
    private readonly EmployeeScenariosService _employeeScenariosService;

    public SSContributionCalc(SSCeilingService ssCeilingService, EmployeeScenariosService employeeScenariosService)
    {   
        if (ssCeilingService == null)
        {
            throw new ArgumentNullException(nameof(ssCeilingService), "SSCeilingService cannot be null.");
        }

        if (employeeScenariosService == null)
        {
            throw new ArgumentNullException(nameof(employeeScenariosService), "EmployeeScenariosService cannot be null.");
        }

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
        var ssCeiling = await _ssCeilingService.GetValueAsync(scenario.Year);   
        
        if (GrossSalary > ssCeiling.Ceiling)
        {
            return ssCeiling.Ceiling;
        }

        return GrossSalary;
    }

}
