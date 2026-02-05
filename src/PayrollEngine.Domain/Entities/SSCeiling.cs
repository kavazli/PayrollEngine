using System;

namespace PayrollEngine.Domain.Entities;

public class SSCeiling
{
    public Guid Id { get; set; }
    public decimal Year { get; set; }
    public decimal Ceiling { get; set; }
}
