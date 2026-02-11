using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Providers;

public class SSCeilingProvider
{
    readonly private PayrollEngineDbContext _context;

    public SSCeilingProvider(PayrollEngineDbContext context)
    {
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
    }

    public SSCeiling GetValue(decimal year)
    {
        var temp = _context.SSCeilings.SingleOrDefault(item => item.Year == year);

        if(temp == null)
        {
            throw new InvalidOperationException($"SSCeiling not found for year {year}.");          
        }

        return temp;
    }
}
