using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Providers;

public class StampTaxProvider
{
    readonly private PayrollEngineDbContext _context;

    public StampTaxProvider(PayrollEngineDbContext context)
    {
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
    }

    public StampTax GetValue(decimal year)
    {
        var temp = _context.StampTaxes.SingleOrDefault(item => item.Year == year);

        if(temp == null)
        {
            throw new InvalidOperationException($"StampTax not found for year {year}.");          
        }

        return temp;
    }
}
