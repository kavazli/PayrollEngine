using System;

namespace PayrollEngine.Domain.Entities;

public class CumulativeIncomeTaxBase
{
    public Guid Id { get; set; }
    public int Month { get; set; }
    public decimal CumulativeBase { get; set; }
}
