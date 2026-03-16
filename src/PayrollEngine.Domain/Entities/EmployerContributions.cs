using System;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Domain.Entities;

public class EmployerContributions
{
    public Guid Id { get; set; }
    public Months Month { get; set; }
    public decimal EmployerSSContributionAmount { get; set; }
    public decimal EmployerUIContributionAmount { get; set; }
    public decimal TotalEmployerCost { get; set; }
}
