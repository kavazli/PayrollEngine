using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
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


    public async Task<CumulativeIncomeTaxBase> GetValueAsync(Months month)
    {
        var result = await _context.CumulativeIncomeTaxBases.SingleOrDefaultAsync(item => item.Month == month);

        if(result == null)
        {
            throw new InvalidOperationException($"CumulativeIncomeTaxBase not found for month {month}.");          
        }

        return result;
    }


    public async Task<CumulativeIncomeTaxBase> AddAsync(CumulativeIncomeTaxBase taxBase)
    {
        if (taxBase == null)
        {
            throw new ArgumentNullException(nameof(taxBase));
        }

        _context.CumulativeIncomeTaxBases.Add(taxBase);
        await _context.SaveChangesAsync();

        return taxBase;
    }
    
    public async Task ClearAsync()
    {
        var taxBases = await _context.CumulativeIncomeTaxBases.ToListAsync();
        _context.CumulativeIncomeTaxBases.RemoveRange(taxBases);
        await _context.SaveChangesAsync();
   
    }

    
}
