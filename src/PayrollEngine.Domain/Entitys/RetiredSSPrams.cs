using System;

namespace PayrollEngine.Domain.Entitys;

public class RetiredSSPrams
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public decimal EmployeeeSSRate { get; set; }
    public decimal EmployerSSRate { get; set; }
}
