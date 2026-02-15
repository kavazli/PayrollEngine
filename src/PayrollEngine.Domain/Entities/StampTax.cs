using System;

namespace PayrollEngine.Domain.Entities;

public class StampTax
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public decimal Rate { get; set; }
}
