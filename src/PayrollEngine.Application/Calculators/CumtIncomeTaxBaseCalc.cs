using System;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Application.Calculators;

public class CumtIncomeTaxBaseCalc
{
    private readonly CumulativeIncomeTaxBaseService cumulativeIncomeTaxBaseService;

    public CumtIncomeTaxBaseCalc(CumulativeIncomeTaxBaseService cumtIncomeTaxBaseService)
    {
        if (cumtIncomeTaxBaseService == null)
        {
            throw new ArgumentNullException(nameof(cumtIncomeTaxBaseService), "CumulativeIncomeTaxBaseService cannot be null.");
        }

        cumulativeIncomeTaxBaseService = cumtIncomeTaxBaseService;
    }


    public async Task<decimal> Calc(Months months, decimal incomeTaxBase)
    {
        var cumulativeIncomeTaxBase = await cumulativeIncomeTaxBaseService.GetCumulativeIncomeTaxBase(months);
        return cumulativeIncomeTaxBase;
    }


}
