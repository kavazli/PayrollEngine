using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Infrastructure.Providers;

public class ActiveSSParamsProvider : IActiveSSParamsProvider
{   
    private readonly PayrollEngineDbContext _context;
    public ActiveSSParamsProvider(PayrollEngineDbContext context)
    {   
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;    
    }

    public async Task<ActiveSSParams> GetValueAsync(int year)
    {   
        var result = await _context.ActiveSSParams.SingleOrDefaultAsync(item => item.Year == year);

        if(result == null)
        {
            throw new InvalidOperationException($"ActiveSSParams not found for year {year}.");          
        }

        return result;
        
    }
}
