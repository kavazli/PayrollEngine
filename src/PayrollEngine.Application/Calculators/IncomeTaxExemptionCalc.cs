using System;
using PayrollEngine.Application.Services;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Application.Calculators;

public class IncomeTaxExemptionCalc
{

    private readonly MinimumWageService _minimumWageService;
    private readonly IncomeTaxBracketsService _incomeTaxBracketsService;
    private readonly EmployeeScenariosService _employeeScenariosService;

    public IncomeTaxExemptionCalc(MinimumWageService minimumWageService, 
                                  IncomeTaxBracketsService incomeTaxBracketsService, 
                                  EmployeeScenariosService employeeScenariosService)
    {

        if (incomeTaxBracketsService == null)
        {
            throw new ArgumentNullException(nameof(incomeTaxBracketsService), "IncomeTaxBracketsService cannot be null.");
        }

        if (minimumWageService == null)
        {
            throw new ArgumentNullException(nameof(minimumWageService), "MinimumWageService cannot be null.");
        }

        if (employeeScenariosService == null)
        {
            throw new ArgumentNullException(nameof(employeeScenariosService), "EmployeeScenariosService cannot be null.");
        }

        _minimumWageService = minimumWageService;
        _incomeTaxBracketsService = incomeTaxBracketsService;
        _employeeScenariosService = employeeScenariosService;
    }

    public async Task<decimal> Calc(Months months)
    {   

        var scanario = await _employeeScenariosService.GetAsync();
        if (scanario == null)
        {
            throw new InvalidOperationException($"Employee scenario not found.");
        }


        var minimumWage = await _minimumWageService.GetValueAsync(scanario.Year);
        if (minimumWage == null)
        {
            throw new InvalidOperationException($"Minimum wage not found for year: {scanario.Year}");
        }

        var IncomeTaxBrackets = await _incomeTaxBracketsService.GetValueAsync(scanario.Year);
        if (IncomeTaxBrackets == null)
        {
            throw new InvalidOperationException($"Income tax brackets not found for year: {scanario.Year}");
        }

        decimal exemptionBase = minimumWage.NetAmount * (int)months;

        if(exemptionBase <= IncomeTaxBrackets[0].MaxAmount)
        {
            return minimumWage.NetAmount * IncomeTaxBrackets[0].Rate;
        }
        else if(exemptionBase > IncomeTaxBrackets[0].MaxAmount && (exemptionBase - IncomeTaxBrackets[0].MaxAmount) <= minimumWage.NetAmount )
        {
            decimal calc1 = (exemptionBase - IncomeTaxBrackets[0].MaxAmount) * IncomeTaxBrackets[1].Rate;
            decimal calc2 = (minimumWage.NetAmount - (exemptionBase - IncomeTaxBrackets[0].MaxAmount)) * IncomeTaxBrackets[0].Rate;
            return calc1 + calc2;
        }
        else if(exemptionBase > IncomeTaxBrackets[0].MaxAmount && (exemptionBase - IncomeTaxBrackets[0].MaxAmount) > minimumWage.NetAmount)
        {
            return minimumWage.NetAmount * IncomeTaxBrackets[1].Rate;
        }
        
        return 0m;
        
    }


}
