using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Providers;

public class MinimumWageProvider
{
    readonly private PayrollEngineDbContext _context;

    public MinimumWageProvider(PayrollEngineDbContext context)
    {
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
    }

    public MinimumWage GetValue(decimal year)
    {
        var temp = _context.MinimumWages.SingleOrDefault(item => item.Year == year);

        if(temp == null)
        {
            throw new InvalidOperationException($"MinimumWage not found for year {year}.");          
        }

        return temp;
    }

}
