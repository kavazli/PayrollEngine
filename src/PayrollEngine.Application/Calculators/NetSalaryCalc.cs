using System;

namespace PayrollEngine.Application.Calculators;

public class NetSalaryCalc
{
    

    

    public decimal Calc(decimal grossSalary,
                         decimal employeeSSContributionAmount,
                         decimal employeeUIContributionAmount,
                         decimal incomeTax,
                         decimal stampTax)
    {
        var totalDeductions = employeeSSContributionAmount + employeeUIContributionAmount + incomeTax + stampTax;
        var netSalary = grossSalary - totalDeductions;

        return netSalary;
    }

}
