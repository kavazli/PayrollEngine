using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Providers;

public class RetiredSSParamsProvider
{
    readonly private PayrollEngineDbContext _context;

    public RetiredSSParamsProvider(PayrollEngineDbContext context)
    {
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
    }


    public RetiredSSParams GetValue(decimal year)
    {
        var temp = _context.RetiredSSParams.SingleOrDefault(item => item.Year == year);

        if(temp == null)
        {
            throw new InvalidOperationException($"RetiredSSParams not found for year {year}.");          
        }

        return temp;
    }
}
