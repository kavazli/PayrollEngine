using System;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Domain;

public class EmployeeScenario
{
    public int Year { get; set; }
    public EmployeeType EmployeeType { get; set; }
    public Status Status { get; set; }
    public DisabilityDegree DisabilityDegree { get; set; }
    public PayType PayType { get; set; }
    public IncentiveType IncentiveType { get; set; }
    
}
