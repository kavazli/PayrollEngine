using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Infrastructure.Providers;

public class MinimumWageProvider : IMinimumWageProvider
{
    private readonly PayrollEngineDbContext _context;

    public MinimumWageProvider(PayrollEngineDbContext context)
    {
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
    }

    public async Task<MinimumWage> GetValueAsync(int year)
    {
        var result = await _context.MinimumWages.SingleOrDefaultAsync(item => item.Year == year);

        if(result == null)
        {
            throw new InvalidOperationException($"MinimumWage not found for year {year}.");          
        }

        return result;
    }

}
