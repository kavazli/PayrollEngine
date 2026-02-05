using System;

namespace PayrollEngine.Domain.Entitys;

public class IncomeTaxBracket
{
    public int Year { get; set; }
    public int Bracket { get; set; }
    public decimal MinAmount { get; set; }
    public decimal MaxAmount { get; set; }
    public decimal Rate { get; set; }
}
