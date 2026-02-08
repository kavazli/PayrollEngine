using System;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Domain.Entities;

public class EmployeeScenario
{
    
    public Guid Id { get; set; }
    public SalaryInputType SalaryInputType { get; set; }
    public Status Status { get; set; }
    public DisabilityDegree DisabilityDegree { get; set; }
    public PayType PayType { get; set; }
    public IncentiveType IncentiveType { get; set; }
    
}
