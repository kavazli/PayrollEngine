using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Infrastructure.Providers;

public class StampTaxProvider : IStampTaxProvider
{
    private readonly PayrollEngineDbContext _context;

    public StampTaxProvider(PayrollEngineDbContext context)
    {
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
    }

    public async Task<StampTax> GetValueAsync(int year)
    {
        var result = await _context.StampTaxes.SingleOrDefaultAsync(item => item.Year == year);

        if(result == null)
        {
            throw new InvalidOperationException($"StampTax not found for year {year}.");          
        }

        return result;
    }
}
