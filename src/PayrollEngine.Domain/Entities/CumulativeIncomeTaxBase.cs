using System;

namespace PayrollEngine.Domain.Entities;

public class CumulativeIncomeTaxBase
{
    public Guid Id { get; set; }
    public decimal Month { get; set; }
    public decimal CumulativeBase { get; set; }
}
