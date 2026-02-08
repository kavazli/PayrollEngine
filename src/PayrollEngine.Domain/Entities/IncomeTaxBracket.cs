using System;

namespace PayrollEngine.Domain.Entities;

public class IncomeTaxBracket
{
    public Guid Id { get; set; }
    public decimal Year { get; set; }
    public decimal Bracket { get; set; }
    public decimal MinAmount { get; set; }
    public decimal MaxAmount { get; set; }
    public decimal Rate { get; set; }
}
