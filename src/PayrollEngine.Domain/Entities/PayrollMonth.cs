using System;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Domain.Entities;

public class PayrollMonth
{   
    public Guid Id { get; set; }
    public Months Month { get; set; }
    public int WorkDay { get; set; }
    public decimal BaseSalary { get; set; }
    public decimal Overtime50 { get; set; }
    public decimal Overtime100 { get; set; }
    public decimal BonusAmount { get; set; }
    public decimal GrossSalary { get; set; }
    
    public decimal PrivateHealthInsurance { get; set; }
    public decimal ShoppingVoucher { get; set; } 
}
