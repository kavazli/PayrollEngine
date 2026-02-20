using System;

namespace PayrollEngine.Application.Calculators;

public class IncomeTaxBaseCalc
{

    public decimal Calc(decimal GrossSalary, decimal EmployeeSSCont, decimal EmployeeUICont)
    {
        return GrossSalary - (EmployeeSSCont + EmployeeUICont);
    }

}
