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
    private readonly DisabilityDegreeCalc _disabilityDegreeCalc;

    public IncomeTaxBaseCalc(MinimumWageService minimumWageService, 
                             IEmployeeScenariosService employeeScenariosService,
                             DisabilityDegreeCalc disabilityDegreeCalc)
    {   
        if (minimumWageService == null)
        {
            throw new ArgumentNullException(nameof(minimumWageService));
        }
        if (employeeScenariosService == null)
        {
            throw new ArgumentNullException(nameof(employeeScenariosService));
        }
        if (disabilityDegreeCalc == null)
        {
            throw new ArgumentNullException(nameof(disabilityDegreeCalc));
        }



        _employeeScenariosService = employeeScenariosService;
        _minimumWageService = minimumWageService;
        _disabilityDegreeCalc = disabilityDegreeCalc;
    }

    public async Task<decimal> Calc(decimal GrossSalary, decimal EmployeeSSCont, decimal EmployeeUICont)
    {   

        var scenario = await _employeeScenariosService.GetAsync();
        var minimumWage = await _minimumWageService.GetValueAsync(scenario.Year);

        var temp = GrossSalary - (EmployeeSSCont + EmployeeUICont);

        var result = await _disabilityDegreeCalc.Calc(scenario, temp);

        if(result < minimumWage.NetAmount)
        {
            return Math.Round(minimumWage.NetAmount, 2);
        }
        


        return Math.Round(result, 2);
    }

}
