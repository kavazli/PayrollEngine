using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;


namespace PayrollEngine.Infrastructure.Providers;

public class CumulativeIncomeTaxBaseProvider : ICumulativeIncomeTaxBaseProvider
{   

    private readonly PayrollEngineDbContext _context;
    public CumulativeIncomeTaxBaseProvider(PayrollEngineDbContext context)
    {   
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
        
    }

    public async Task<CumulativeIncomeTaxBase> GetValueAsync(int month)
    {
        var result = await _context.CumulativeIncomeTaxBases.SingleOrDefaultAsync(item => item.Month == month);

        if(result == null)
        {
            throw new InvalidOperationException($"CumulativeIncomeTaxBase not found for month {month}.");          
        }

        return result;
    }

}
