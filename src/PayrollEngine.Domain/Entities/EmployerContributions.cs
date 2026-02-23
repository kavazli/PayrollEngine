using System;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Domain.Entities;

public class EmployerContributions
{
    public Guid Id { get; set; }
    public Months Month { get; set; }
    public decimal EmployeeSSContributionAmount { get; set; }
    public decimal EmployeeUIContributionAmount { get; set; }
    public decimal TotalEmployerCost { get; set; }
}
