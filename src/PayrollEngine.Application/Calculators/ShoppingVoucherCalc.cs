using System;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Application.Services.Services;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces;
using PayrollEngine.Domain.Interfaces.Services;

namespace PayrollEngine.Application.Calculators;

public class ShoppingVoucherCalc
{
    private readonly IResultPayrollService _resultPayrollService;
    private readonly IShoppingVoucherService _shoppingVoucherService;
    private readonly StampTaxService _stampTaxService;
    private readonly IEmployeeScenariosService _employeeScenariosService;

    public ShoppingVoucherCalc(IResultPayrollService resultPayrollService,
                             IShoppingVoucherService shoppingVoucherService,
                             StampTaxService stampTaxService,
                             IEmployeeScenariosService employeeScenariosService)
    {   

        if (resultPayrollService == null)
        {
            throw new ArgumentNullException(nameof(resultPayrollService), "ResultPayrollService cannot be null.");
        }

        if (shoppingVoucherService == null)
        {
            throw new ArgumentNullException(nameof(shoppingVoucherService), "ShoppingVoucherService cannot be null.");
        }

        if ( stampTaxService == null)
        {
            throw new ArgumentNullException(nameof(stampTaxService), "StampTaxService cannot be null.");
        }

        if (employeeScenariosService == null)
        {
            throw new ArgumentNullException(nameof(employeeScenariosService), "EmployeeScenariosService cannot be null.");
        }

        _resultPayrollService = resultPayrollService;
        _shoppingVoucherService = shoppingVoucherService;
        _stampTaxService = stampTaxService;
        _employeeScenariosService = employeeScenariosService;
    }


    public async Task<ShoppingVoucher> Calc(PayrollMonth currentMonth, Months months)
    {   
        if (currentMonth == null)
        {
            throw new ArgumentNullException(nameof(currentMonth), "Current payroll month is not available.");
        }

        var resultPayroll = await _resultPayrollService.GetMonthAsync(months);
        if (resultPayroll == null)
        {
            throw new InvalidOperationException($"Result payroll for month {months} is not available.");
        }

        var scenario = await _employeeScenariosService.GetAsync();
        var stampTax = await _stampTaxService.GetValueAsync(scenario.Year);

        // Rate'leri sakla
        decimal incomeTaxRate = resultPayroll.IncomeTaxRate;
        decimal stampTaxRate = stampTax.Rate;
        
        decimal targetNet = currentMonth.ShoppingVoucher;  // Hedef net tutar
        decimal grossGuess = targetNet / (1 - incomeTaxRate - stampTaxRate);  // Başlangıç tahmini

        // Newton-Raphson benzeri iterasyon
        for (int i = 0; i < 200; i++)
        {
            decimal incomeTaxAmount = grossGuess * incomeTaxRate;
            decimal stampTaxAmount = grossGuess * stampTaxRate;
            decimal netAmount = grossGuess - incomeTaxAmount - stampTaxAmount;
            
            decimal difference = targetNet - netAmount;
            
            if (Math.Abs(difference) < 0.01m)
                break;
            
            grossGuess += difference;
        }

        return new ShoppingVoucher
        {
            Id = Guid.NewGuid(),
            Month = currentMonth.Month,
            GrossAmount = Math.Round(grossGuess, 2),
            IncomeTax = Math.Round(grossGuess * incomeTaxRate, 2),
            StampTax = Math.Round(grossGuess * stampTaxRate, 2),
            NetAmount = Math.Round(targetNet, 2)
        };
    }

}
