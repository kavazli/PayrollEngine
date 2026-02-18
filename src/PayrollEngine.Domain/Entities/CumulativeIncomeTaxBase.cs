using System;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Domain.Entities;

public class CumulativeIncomeTaxBase
{
    public Guid Id { get; set; }
    public Months Month { get; set; }
    public decimal CumulativeBase { get; set; }
}
