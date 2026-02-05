using System;

namespace PayrollEngine.Domain.Entitys;

public class ActiveSSPrams
{
    public Guid Id { get; set; }
    public int Year { get; set; }
    public decimal EmployeeSSRate { get; set; }
    public decimal EmployeeUIRate { get; set; }
    public decimal EmployerSSRate { get; set; }
    public decimal EmployerUIRate { get; set; }
}
