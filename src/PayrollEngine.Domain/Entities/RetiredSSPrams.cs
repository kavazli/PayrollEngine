using System;

namespace PayrollEngine.Domain.Entities;

public class RetiredSSPrams
{
    public Guid Id { get; set; }
    public decimal Year { get; set; }
    public decimal EmployeeSSRate { get; set; }
    public decimal EmployerSSRate { get; set; }
}
