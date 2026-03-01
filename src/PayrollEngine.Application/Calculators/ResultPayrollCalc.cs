using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Application.Calculators;

public class ResultPayrollCalc
{
    private readonly CumtIncomeTaxBaseCalc _cumtIncomeTaxBaseCalc;
    private readonly DisabilityDegreeCalc _disabilityDegreeCalc;
    private readonly EmployeeSSCAmountCalc _employeeSSCAmountCalc;
    private readonly EmployeeUICAmountCalc _employeeUICAmountCalc;
    private readonly IncomeTaxBaseCalc _incomeTaxBaseCalc;
    private readonly IncomeTaxCalc _incomeTaxCalc;
    private readonly IncomeTaxExemptionCalc _incomeTaxExemptionCalc;
    private readonly NetSalaryCalc _netSalaryCalc;
    private readonly SSContributionCalc _sSContributionCalc;
    private readonly StampTaxCalc _stampTaxCalc;
    private readonly StampTaxExemptionCalc _stampTaxExemptionCalc;

    public ResultPayrollCalc(CumtIncomeTaxBaseCalc cumtIncomeTaxBaseCalc,
                             DisabilityDegreeCalc disabilityDegreeCalc,
                             EmployeeSSCAmountCalc employeeSSCAmountCalc,
                             EmployeeUICAmountCalc employeeUICAmountCalc,
                             IncomeTaxBaseCalc incomeTaxBaseCalc,
                             IncomeTaxCalc incomeTaxCalc,
                             IncomeTaxExemptionCalc incomeTaxExemptionCalc,
                             NetSalaryCalc netSalaryCalc,
                             SSContributionCalc sSContributionCalc,
                             StampTaxCalc stampTaxCalc,
                             StampTaxExemptionCalc stampTaxExemptionCalc)
    {
        _cumtIncomeTaxBaseCalc = cumtIncomeTaxBaseCalc;
        _disabilityDegreeCalc = disabilityDegreeCalc;
        _employeeSSCAmountCalc = employeeSSCAmountCalc;
        _employeeUICAmountCalc = employeeUICAmountCalc;
        _incomeTaxBaseCalc = incomeTaxBaseCalc;
        _incomeTaxCalc = incomeTaxCalc;
        _incomeTaxExemptionCalc = incomeTaxExemptionCalc;
        _netSalaryCalc = netSalaryCalc;
        _sSContributionCalc = sSContributionCalc;
        _stampTaxCalc = stampTaxCalc;
        _stampTaxExemptionCalc = stampTaxExemptionCalc;
    }


    public async Task<ResultPayroll> Calc(PayrollMonth payrollMonth)
    {
        
        var grossSalary = payrollMonth.GrossSalary;

            // 1. SGK Matrahı
            var ssContributionBase = await _sSContributionCalc.Calc(grossSalary);

            // 2. Çalışan SGK ve İşsizlik Sigortası Kesintileri
            var employeeSSCAmount = await _employeeSSCAmountCalc.Calc(ssContributionBase);
            var employeeUICAmount = await _employeeUICAmountCalc.Calc(ssContributionBase);

            // 3. Gelir Vergisi Matrahı
            var incomeTaxBase = await _incomeTaxBaseCalc.Calc(grossSalary, employeeSSCAmount, employeeUICAmount);

            // 4. Kümülatif Gelir Vergisi Matrahı
            var cumulativeIncomeTaxBase = await _cumtIncomeTaxBaseCalc.Calc(payrollMonth.Month, incomeTaxBase);

            // 5. Gelir Vergisi ve Asgari Geçim İndirimi
            var incomeTaxExemption = await _incomeTaxExemptionCalc.Calc(payrollMonth.Month);
            var incomeTax = await _incomeTaxCalc.Calc(payrollMonth.Month);

            // 6. Damga Vergisi ve Muafiyeti
            var stampTaxExemption = await _stampTaxExemptionCalc.Calc();
            var stampTax = await _stampTaxCalc.Calc(grossSalary);

            // 7. Net Ücret
            var netSalary = _netSalaryCalc.Calc(grossSalary, employeeSSCAmount, employeeUICAmount, incomeTax, stampTax);

            var resultPayroll = new ResultPayroll
            {
                Month = payrollMonth.Month,
                WorkDay = payrollMonth.WorkDay,
                SSDay = payrollMonth.WorkDay,
                CurrentSalary = payrollMonth.BaseSalary,
                Overtime50Amount = payrollMonth.Overtime50,
                Overtime100Amount = payrollMonth.Overtime100,
                BonusAmount = payrollMonth.BonusAmount,
                TotalSalary = payrollMonth.BaseSalary + payrollMonth.Overtime50 + payrollMonth.Overtime100 + payrollMonth.BonusAmount,
                GrossSalary = grossSalary,
                SSContributionBase = ssContributionBase,
                EmployeeSSContributionAmount = employeeSSCAmount,
                EmployeeUIContributionAmount = employeeUICAmount,
                IncomeTaxBase = incomeTaxBase,
                CumulativeIncomeTaxBase = cumulativeIncomeTaxBase,
                IncomeTaxExemption = incomeTaxExemption,
                IncomeTax = incomeTax,
                StampTaxExemption = stampTaxExemption,
                StampTax = stampTax,
                NetSalary = netSalary,
            };

            return resultPayroll;
        
        
       
    }
}
