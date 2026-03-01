using System;
using PayrollEngine.Application.Calculators;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Application;

public class NetSalaryIteration
{   

    private readonly ResultPayrollCalc _resultPayrollCalc;

    public NetSalaryIteration(ResultPayrollCalc resultPayrollCalc)
    {   
        if(resultPayrollCalc == null)
        {
            throw new ArgumentNullException(nameof(resultPayrollCalc));
        }
        _resultPayrollCalc = resultPayrollCalc;
    }    

    public async Task<ResultPayroll> Iterator(PayrollMonth payrollMonth)
    {
        // payrollMonth.GrossSalary aslında hedef net ücret; brüt iterasyonla bulunacak
        decimal targetNet = payrollMonth.GrossSalary;

        // İlk brüt tahmini hedef net ile başlar (net ≤ brüt olduğundan alt sınır)
        decimal grossGuess = targetNet;

        // Orijinal nesneyi bozmamak için kopya üzerinde çalış
        var workingMonth = new PayrollMonth
        {
            Id = payrollMonth.Id,
            Month = payrollMonth.Month,
            WorkDay = payrollMonth.WorkDay,
            BaseSalary = payrollMonth.BaseSalary,
            Overtime50 = payrollMonth.Overtime50,
            Overtime100 = payrollMonth.Overtime100,
            BonusAmount = payrollMonth.BonusAmount,
            GrossSalary = grossGuess
        };

        ResultPayroll tempResultPayroll = new ResultPayroll();

        for (int i = 0; i < 200; i++)
        {
            workingMonth.GrossSalary = grossGuess;
            tempResultPayroll = await _resultPayrollCalc.Calc(workingMonth);

            // İşaretli fark: pozitif → brüt düşük (artır), negatif → brüt yüksek (azalt)
            decimal fark = targetNet - tempResultPayroll.NetSalary;

            if (Math.Abs(fark) < 0.01m)
            {
                break;
            }

            grossGuess += fark;
        }

        return tempResultPayroll;
    }

}
