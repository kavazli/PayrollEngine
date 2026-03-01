

using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;


namespace PayrollEngine.Application.Calculators;


public class CumtIncomeTaxBaseCalc
{
    private readonly CumulativeIncomeTaxBaseService _cumulativeIncomeTaxBaseService;

    public CumtIncomeTaxBaseCalc(CumulativeIncomeTaxBaseService cumulativeIncomeTaxBaseService)
    {
        if (cumulativeIncomeTaxBaseService == null)
        {
            throw new ArgumentNullException(nameof(cumulativeIncomeTaxBaseService), "CumulativeIncomeTaxBaseService cannot be null.");
        }

        _cumulativeIncomeTaxBaseService = cumulativeIncomeTaxBaseService;
    }


    public async Task<decimal> Calc(Months currentMonth, decimal incomeTaxBase)
    {
        decimal previousCumulativeBase = 0m;

        // Ocak değilse bir önceki ayın kümülatif matrahını DB'den çek
        if (currentMonth != Months.January)
        {
            var previousMonth = (Months)((int)currentMonth - 1);
            
            var previousRecord = await _cumulativeIncomeTaxBaseService.GetValueAsync(previousMonth);
            if (previousRecord != null)
            {
                previousCumulativeBase = previousRecord.CumulativeBase;
            }
        }

        var newCumulativeBase = previousCumulativeBase + incomeTaxBase;

        // Mevcut ayın kümülatif matrahını DB'ye upsert et (iterasyonda mükerrer kayıt oluşmasını önler)
        var existingRecord = await _cumulativeIncomeTaxBaseService.GetValueAsync(currentMonth);
        if (existingRecord != null)
        {
            existingRecord.CumulativeBase = newCumulativeBase;
            await _cumulativeIncomeTaxBaseService.UpdateAsync(existingRecord);
        }
        else
        {
            var newRecord = new CumulativeIncomeTaxBase
            {
                Id = Guid.NewGuid(),
                Month = currentMonth,
                CumulativeBase = newCumulativeBase
            };
            await _cumulativeIncomeTaxBaseService.AddAsync(newRecord);
        }

        return Math.Round(newCumulativeBase, 2);
    }


}
