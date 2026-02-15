using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Infrastructure.Providers;

public class RetiredSSParamsProvider : IRetiredSSParamsProvider
{
    private readonly PayrollEngineDbContext _context;

    public RetiredSSParamsProvider(PayrollEngineDbContext context)
    {
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
    }


    public async Task<RetiredSSParams> GetValueAsync(int year)
    {
        var result = await _context.RetiredSSParams.SingleOrDefaultAsync(item => item.Year == year);

        if(result == null)
        {
            throw new InvalidOperationException($"RetiredSSParams not found for year {year}.");          
        }

        return result;
    }
}
