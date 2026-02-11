using System;
using System.Linq;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Providers;

public class ActiveSSParamsProvider
{   
    readonly private PayrollEngineDbContext _context;
    public ActiveSSParamsProvider(PayrollEngineDbContext context)
    {   
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;    
    }

    public ActiveSSParams GetValue(decimal year)
    {   
        var temp = _context.ActiveSSParams.SingleOrDefault(item => item.Year == year);

        if(temp == null)
        {
            throw new InvalidOperationException($"ActiveSSParams not found for year {year}.");          
        }

        return temp;
        
    }
}
