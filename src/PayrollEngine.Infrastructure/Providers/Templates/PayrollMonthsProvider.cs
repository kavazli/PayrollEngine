using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Providers;


namespace PayrollEngine.Infrastructure.Providers.Templates;

public class PayrollMonthsProvider : IPayrollMonthsProvider
{

    private readonly PayrollEngineDbContext _context;

    public PayrollMonthsProvider(PayrollEngineDbContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        _context = context;
    }


    public async Task<List<PayrollMonth>> AddAsync(List<PayrollMonth> months)
    {
        if (months == null)
        {
            throw new ArgumentNullException(nameof(months));
        }

        await _context.PayrollMonths.AddRangeAsync(months);
        await _context.SaveChangesAsync();
        return months;
    }
    

    public async Task<List<PayrollMonth>> GetAsync()
    {
        return await _context.PayrollMonths.ToListAsync();
    }


    public async Task ClearAsync()
    {
        var months = await _context.PayrollMonths.ToListAsync();
        _context.PayrollMonths.RemoveRange(months);
        await _context.SaveChangesAsync();
    }
    

    public async Task SetAsync(List<PayrollMonth> months)
    {
        if (months == null)
        {
            throw new ArgumentNullException(nameof(months));
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var existingMonths = await _context.PayrollMonths.ToListAsync();
            _context.PayrollMonths.RemoveRange(existingMonths);
            
            await _context.PayrollMonths.AddRangeAsync(months);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

}
