using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Infrastructure.Providers;

public class IncomeTaxBracketsProvider : IIncomeTaxBracketsProvider
{
    private readonly PayrollEngineDbContext _context;

    public IncomeTaxBracketsProvider(PayrollEngineDbContext context)
    {
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
    }

    public async Task<List<IncomeTaxBracket>> GetValueAsync(int year)
    {
        var result = await _context.IncomeTaxBrackets.Where(b => b.Year == year).ToListAsync();

        if(result == null || result.Count == 0)
        {
            throw new InvalidOperationException($"No IncomeTaxBrackets found for year {year}.");          
        }

        return result.OrderBy(b => b.MinAmount).ToList();
    }

}
