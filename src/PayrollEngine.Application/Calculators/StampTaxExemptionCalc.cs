using System;
using PayrollEngine.Application.Services;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Services;

namespace PayrollEngine.Application.Calculators;

public class StampTaxExemptionCalc
{
    private readonly MinimumWageService _minimumWageService;
    private readonly IEmployeeScenariosService _employeeScenariosService;
    private readonly StampTaxService _stampTaxService;  

    public StampTaxExemptionCalc(MinimumWageService minimumWageService, 
                                 IEmployeeScenariosService employeeScenariosService,
                                 StampTaxService stampTaxService)
    {
        if(minimumWageService == null)
        {
            throw new ArgumentNullException(nameof(minimumWageService));
        }
        if(employeeScenariosService == null)
        {
            throw new ArgumentNullException(nameof(employeeScenariosService));
        }
        if(stampTaxService == null)
        {
            throw new ArgumentNullException(nameof(stampTaxService));
        }


        _minimumWageService = minimumWageService;
        _employeeScenariosService = employeeScenariosService;
        _stampTaxService = stampTaxService;
    }

    public async Task<decimal> Calc()
    {
        var scenario = await _employeeScenariosService.GetAsync();

        if (scenario == null)
        {
            throw new InvalidOperationException($"Employee scenario not found.");
        }

        var minimumWage = await _minimumWageService.GetValueAsync(scenario.Year);
        var stampTax = await _stampTaxService.GetValueAsync(scenario.Year);
        decimal result;

        if (minimumWage == null)
        {
            throw new InvalidOperationException($"Minimum wage not found.");
        }

        if (stampTax == null)
        {
            throw new InvalidOperationException($"Stamp tax not found for year: {scenario.Year}");
        }

        result = minimumWage.GrossAmount * stampTax.Rate;

        return Math.Round(result, 2);

    }


}
