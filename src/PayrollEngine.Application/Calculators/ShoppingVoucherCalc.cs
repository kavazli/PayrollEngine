using System;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces.Services;

namespace PayrollEngine.Application.Calculators;

public class ShoppingVoucherCalc
{
    private readonly IPayrollMonthService _payrollMonthService;
    private readonly IResultPayrollService _resultPayrollService;
    private readonly IShoppingVoucherService _shoppingVoucherService;

    public ShoppingVoucherCalc(IPayrollMonthService payrollMonthService, 
                             IResultPayrollService resultPayrollService,
                             IShoppingVoucherService shoppingVoucherService)
    {   

        if (payrollMonthService == null)
        {
            throw new ArgumentNullException(nameof(payrollMonthService), "PayrollMonthService cannot be null.");
        }

        if (resultPayrollService == null)
        {
            throw new ArgumentNullException(nameof(resultPayrollService), "ResultPayrollService cannot be null.");
        }

        if (shoppingVoucherService == null)
        {
            throw new ArgumentNullException(nameof(shoppingVoucherService), "ShoppingVoucherService cannot be null.");
        }

        _payrollMonthService = payrollMonthService;
        _resultPayrollService = resultPayrollService;
        _shoppingVoucherService = shoppingVoucherService;
    }


    public async Task<decimal> Calc(Months months)
    {   
        var currentMonth = await _payrollMonthService.GetMonthAsync(months);
        if (currentMonth == null)
        {
            throw new InvalidOperationException("Current payroll month is not available.");
        }

        var resultPayroll = await _resultPayrollService.GetMonthAsync(months);
        if (resultPayroll == null)
        {
            throw new InvalidOperationException($"Result payroll for month {months} is not available.");
        }

        
        return 0;
    }





}
