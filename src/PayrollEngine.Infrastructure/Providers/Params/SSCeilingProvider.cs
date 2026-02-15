using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Infrastructure.Providers;

public class SSCeilingProvider : ISSCeilingProvider
{
    private readonly PayrollEngineDbContext _context;

    public SSCeilingProvider(PayrollEngineDbContext context)
    {
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
    }

    public async Task<SSCeiling> GetValueAsync(int year)
    {
        var result = await _context.SSCeilings.SingleOrDefaultAsync(item => item.Year == year);

        if(result == null)
        {
            throw new InvalidOperationException($"SSCeiling not found for year {year}.");          
        }

        return result;
    }
}
