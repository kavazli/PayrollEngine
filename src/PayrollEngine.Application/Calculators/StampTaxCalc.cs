using System;
using PayrollEngine.Application.Services;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Interfaces.Services;

namespace PayrollEngine.Application.Calculators;

public class StampTaxCalc
{
    private readonly StampTaxService _stampTaxService;
    private readonly IEmployeeScenariosService _employeeScenariosService;
    private readonly StampTaxExemptionCalc _stampTaxExemptionCalc;  
    
    public StampTaxCalc(StampTaxService stampTaxService, 
                        IEmployeeScenariosService employeeScenariosService,
                        StampTaxExemptionCalc stampTaxExemptionCalc)
    {   

        if(stampTaxService == null)
        {
            throw new ArgumentNullException(nameof(stampTaxService));
        }
        if(employeeScenariosService == null)
        {
            throw new ArgumentNullException(nameof(employeeScenariosService));
        }
        if(stampTaxExemptionCalc == null)
        {
            throw new ArgumentNullException(nameof(stampTaxExemptionCalc));
        }

        _stampTaxService = stampTaxService;
        _employeeScenariosService = employeeScenariosService;
        _stampTaxExemptionCalc = stampTaxExemptionCalc;
    }


    public async Task<decimal> Calc(decimal GrossSalary)
    {
        var scenario = await _employeeScenariosService.GetAsync();

        if (scenario == null)
        {
            throw new InvalidOperationException($"Employee scenario not found.");
        }

        var stampTax = await _stampTaxService.GetValueAsync(scenario.Year);

        if (stampTax == null)
        {
            throw new InvalidOperationException($"Stamp tax not found for year: {scenario.Year}");
        }
        
        decimal exemption = await _stampTaxExemptionCalc.Calc();

        if((GrossSalary * stampTax.Rate) < exemption)
        { 
            return 0; // hesaplanan damga vergisi muafiyet tutarından küçükse, damga vergisi sıfır olarak döndürülür.
        }

        decimal result = (GrossSalary * stampTax.Rate) - exemption;


        return result;
    }


}
