using System;

namespace PayrollEngine.Application.Calculators;

public class NetSalaryCalc
{
    private readonly decimal _grossSalary;
    private readonly decimal _employeeSSContributionAmount;
    private readonly decimal _employeeUIContributionAmount;
    private readonly decimal _incomeTax;
    private readonly decimal _stampTax;

    public NetSalaryCalc(decimal grossSalary,
                         decimal employeeSSContributionAmount,
                         decimal employeeUIContributionAmount,
                         decimal incomeTax,
                         decimal stampTax)
    {
        
        _grossSalary = grossSalary;
        _employeeSSContributionAmount = employeeSSContributionAmount;
        _employeeUIContributionAmount = employeeUIContributionAmount;
        _incomeTax = incomeTax;
        _stampTax = stampTax;
    }
    

    public decimal Calc()
    {
        var totalDeductions = _employeeSSContributionAmount + _employeeUIContributionAmount + _incomeTax + _stampTax;
        var netSalary = _grossSalary - totalDeductions;

        return netSalary;
    }

}
