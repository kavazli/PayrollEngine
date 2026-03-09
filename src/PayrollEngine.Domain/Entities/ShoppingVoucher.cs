using System;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Domain.Entities;

public class ShoppingVoucher
{   
    public Guid Id { get; set; }
    public Months Month { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal IncomeTax { get; set; }
    public decimal StampTax { get; set; }
    public decimal NetAmount { get; set; }
}
