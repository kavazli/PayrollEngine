using System;

namespace PayrollEngine.Domain.Entitys;

public class MinimumWage
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal NetAmount { get; set; }
}
