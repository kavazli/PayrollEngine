using System;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Domain.Entities;

public class PrivateHealthInsurance
{   
    public Guid Id { get; set; }
    public Months Month { get; set; }
    public decimal NetAmount { get; set; }
    public decimal GrossAmount { get; set; }
}
