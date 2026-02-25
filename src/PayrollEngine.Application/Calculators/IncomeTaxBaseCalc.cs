using System;
using System.Threading.Tasks;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Services;

namespace PayrollEngine.Application.Calculators;

public class IncomeTaxBaseCalc
{   

    private readonly MinimumWageService _minimumWageService;
    private readonly IEmployeeScenariosService _employeeScenariosService;

    public IncomeTaxBaseCalc(MinimumWageService minimumWageService, IEmployeeScenariosService employeeScenariosService)
    {   
        if (minimumWageService == null)
        {
            throw new ArgumentNullException(nameof(minimumWageService));
        }
        if (employeeScenariosService == null)
        {
            throw new ArgumentNullException(nameof(employeeScenariosService));
        }
        _employeeScenariosService = employeeScenariosService;
        _minimumWageService = minimumWageService;
    }

    public async Task<decimal> Calc(decimal GrossSalary, decimal EmployeeSSCont, decimal EmployeeUICont)
    {   

        var scenario = await _employeeScenariosService.GetAsync();
        var minimumWage = await _minimumWageService.GetValueAsync(scenario.Year);

        var result = GrossSalary - (EmployeeSSCont + EmployeeUICont);

        if(result < minimumWage.NetAmount)
        {
            return minimumWage.NetAmount;
        }

        return result;
    }

}
