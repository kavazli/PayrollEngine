using System;
using PayrollEngine.Application.Services;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Application.Calculators;

public class IncomeTaxCalc
{
    private readonly IncomeTaxBracketsService _incomeTaxService;
    private readonly CumulativeIncomeTaxBaseService _cumulativeIncomeTaxBaseService;

    private readonly EmployeeScenariosService _employeeScenariosService;    

    public IncomeTaxCalc(IncomeTaxBracketsService incomeTaxService, 
                         CumulativeIncomeTaxBaseService cumulativeIncomeTaxBaseService, 
                         EmployeeScenariosService employeeScenariosService)
    {   

        if (incomeTaxService == null)
        {
            throw new ArgumentNullException(nameof(incomeTaxService), "IncomeTaxBracketsService cannot be null.");
        }


        if (cumulativeIncomeTaxBaseService == null)
        {
            throw new ArgumentNullException(nameof(cumulativeIncomeTaxBaseService), "CumulativeIncomeTaxBaseService cannot be null.");
        }

        if (employeeScenariosService == null)
        {
            throw new ArgumentNullException(nameof(employeeScenariosService), "EmployeeScenariosService cannot be null.");
        }

        _incomeTaxService = incomeTaxService;
        _cumulativeIncomeTaxBaseService = cumulativeIncomeTaxBaseService;
        _employeeScenariosService = employeeScenariosService;
    }

    public async Task<decimal> Calc(Months months)
    {   
        // Önce null kontrolü yapılmalı, sonra property erişimi
        var currentCumulativeBase = await _cumulativeIncomeTaxBaseService.GetValueAsync(months);
        if (currentCumulativeBase == null)
        {
            throw new InvalidOperationException($"Cumulative income tax base for month {months} is not available.");
        }

        //Ocak ayında önceki ay (Months)0 geçersizdir, önceki kümülatif matrah 0 kabul edilir
        decimal previousCumulativeBase = 0m;
        if (months != Months.January)
        {
            var previousRecord = await _cumulativeIncomeTaxBaseService.GetValueAsync((Months)((int)months - 1));
            if (previousRecord != null)
            {
                previousCumulativeBase = previousRecord.CumulativeBase;
            }
        }

        var taxPrevious = await CalculateTaxAmount(previousCumulativeBase);
        var taxCurrent = await CalculateTaxAmount(currentCumulativeBase.CumulativeBase);
        return taxCurrent - taxPrevious;
    } 
   
    private async Task<decimal> CalculateTaxAmount(decimal cumulativeBase)
    {   
        var employeeScenario = await _employeeScenariosService.GetAsync();
        if(employeeScenario == null)
        {
            throw new InvalidOperationException("Employee scenario is not available.");
        }

        var taxBrackets = await _incomeTaxService.GetValueAsync(employeeScenario.Year);
        if (taxBrackets == null || taxBrackets.Count == 0)
        {
            throw new InvalidOperationException($"Income tax brackets for the year {employeeScenario.Year} are not available.");
        }

        
        // Eğer toplam matrah, mevcut dilimin minimumundan küçük veya eşitse döngüden çık
        if(cumulativeBase <= 0)
        {
            return 0;
        }

        decimal tax = 0m;


        // Vergi dilimlerini sırayla kontrol et
        for(int i = 0; i <taxBrackets.Count; i++)
        {
            // Mevcut dilimin minimum ve maksimum matrahını belirle
            decimal bracketMin = (i == 0) ? 0 : taxBrackets[i-1].MaxAmount; // İlk dilim için minimum 0, diğerleri için bir önceki dilimin maksimumu
            decimal bracketMax = taxBrackets[i].MaxAmount; // Mevcut dilimin maksimum matrahı

            // Eğer toplam matrah, mevcut dilimin minimumundan küçük veya eşitse döngüden çık
            if(cumulativeBase <= bracketMin)
            {
                break;
            }

            // Matrah bu dilimin üst sınırını aşıyorsa tam dilim, aşmıyorsa kısmi hesapla
            if (cumulativeBase >= bracketMax)
            {
                // Matrah bu dilimin tamamını kapsıyor, tam dilim vergisi ekle
                tax += (bracketMax - bracketMin) * taxBrackets[i].Rate;
            }
            else
            {
                // Matrah bu dilimin ortasında kalıyor, kısmi vergi hesapla ve döngüden çık
                tax += (cumulativeBase - bracketMin) * taxBrackets[i].Rate;
                break;
            }

        }

        return tax;

    }

}