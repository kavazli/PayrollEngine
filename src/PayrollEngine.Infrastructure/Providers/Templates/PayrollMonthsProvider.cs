using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Providers.Templates;

public class PayrollMonthsProvider
{

    readonly private PayrollEngineDbContext _Context;

    public PayrollMonthsProvider(PayrollEngineDbContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        _Context = context;
    }


    public async Task<List<PayrollMonth>> AddAsync(List<PayrollMonth> months)
    {
        if (months == null)
        {
            throw new ArgumentNullException(nameof(months));
        }

        _Context.PayrollMonths.AddRange(months);
        await _Context.SaveChangesAsync();
        return months;
    }
    

    public async Task<List<PayrollMonth>> GetAsync()
    {
        return await _Context.PayrollMonths.ToListAsync();
    }


    public async Task ClearAsync()
    {
        var months = await _Context.PayrollMonths.ToListAsync();
        _Context.PayrollMonths.RemoveRange(months);
        await _Context.SaveChangesAsync();
    }
    

    public async Task SetAsync(List<PayrollMonth> months)
    {
        if (months == null)
        {
            throw new ArgumentNullException(nameof(months));
        }

        using (var transaction = _Context.Database.BeginTransaction())
        {
            try
            {
                var existingMonths = await _Context.PayrollMonths.ToListAsync();
                _Context.PayrollMonths.RemoveRange(existingMonths);
                await _Context.SaveChangesAsync();

                _Context.PayrollMonths.AddRange(months);
                await _Context.SaveChangesAsync();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }

}
