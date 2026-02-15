using System;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Domain.Entities;

public class ResultPayroll
{
    public Guid Id { get; set; }
    public Months Month { get; set; }
    public int WorkDay { get; set; }
    public int SSDay { get; set; }
    public decimal CurrentSalary { get; set; }
    public decimal Overtime50Amount { get; set; }
    public decimal Overtime100Amount { get; set; }
    public decimal BonusAmount { get; set; }
    public decimal TotalSalary { get; set; }
    public decimal PrivateHealthInsurance { get; set; }
    public decimal ShoppingVoucher { get; set; }

    public decimal GrossSalary { get; set; }
    public decimal SSContributionBase { get; set; }
    public decimal EmployeeSSContributionAmount { get; set; }
    public decimal  EmployeeUIContributionAmount { get; set; }
    public decimal CumulativeIncomeTaxBase { get; set; }
    public decimal IncomeTaxBase { get; set; }
    public decimal IncomeTax { get; set; }
    public decimal IncomeTaxExemption { get; set; }
    public decimal StampTax { get; set; }
    public decimal StampTaxExemption { get; set; }
    public decimal NetSalary { get; set; }
    
    public decimal EmployerSSContributionAmount { get; set; }
    public decimal EmployerUIContributionAmount { get; set; }
    public decimal IncentiveDiscount { get; set; }
    public decimal TotalEmployerCost { get; set; }




}
