using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Providers.Templates;

public class ResultPayrollsProvider
{
    readonly private PayrollEngineDbContext _context;

    public ResultPayrollsProvider(PayrollEngineDbContext context)
    {

        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        _context = context;
        
    }


    public async Task<List<ResultPayroll>> AddAsync(List<ResultPayroll> resultPayrolls)
    {
        if (resultPayrolls == null)
        {
            throw new ArgumentNullException(nameof(resultPayrolls));
        }

        _context.ResultPayrolls.AddRange(resultPayrolls);
        await _context.SaveChangesAsync();
        return resultPayrolls;
    }

    public async Task<List<ResultPayroll>> GetAsync()
    {
        return await _context.ResultPayrolls.ToListAsync();
    }

    public void Clear()
    {
        var resultPayrolls = _context.ResultPayrolls.ToList();
        _context.ResultPayrolls.RemoveRange(resultPayrolls);
        _context.SaveChanges();
    }

    public async Task SetAsync(List<ResultPayroll> resultPayrolls)
    {
        if (resultPayrolls == null)
        {
            throw new ArgumentNullException(nameof(resultPayrolls));
        }

        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                var existingResultPayrolls = await _context.ResultPayrolls.ToListAsync();
                _context.ResultPayrolls.RemoveRange(existingResultPayrolls);
                await _context.SaveChangesAsync();

                _context.ResultPayrolls.AddRange(resultPayrolls);
                await _context.SaveChangesAsync();

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
