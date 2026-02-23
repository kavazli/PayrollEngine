using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Templates;

namespace PayrollEngine.Infrastructure.Providers.Templates;

public class ResultPayrollsProvider : IResultPayrollsProvider
{
    private readonly PayrollEngineDbContext _context;

    public ResultPayrollsProvider(PayrollEngineDbContext context)
    {

        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        _context = context;
        
    }

    public async Task<ResultPayroll> AddAsync(ResultPayroll resultPayroll)
    {
        if (resultPayroll == null)
        {
            throw new ArgumentNullException(nameof(resultPayroll));
        }

        await _context.ResultPayrolls.AddAsync(resultPayroll);
        await _context.SaveChangesAsync();
        return resultPayroll;
    }

    public async Task<List<ResultPayroll>> AddRangeAsync(List<ResultPayroll> resultPayrolls)
    {
        if (resultPayrolls == null)
        {
            throw new ArgumentNullException(nameof(resultPayrolls));
        }

        await _context.ResultPayrolls.AddRangeAsync(resultPayrolls);
        await _context.SaveChangesAsync();
        return resultPayrolls;
    }

    public async Task<List<ResultPayroll>> GetAsync()
    {
        return await _context.ResultPayrolls.ToListAsync();
    }

    public async Task ClearAsync()
    {
        var resultPayrolls = await _context.ResultPayrolls.ToListAsync();
        _context.ResultPayrolls.RemoveRange(resultPayrolls);
        await _context.SaveChangesAsync();
    }

    public async Task SetAsync(List<ResultPayroll> resultPayrolls)
    {
        if (resultPayrolls == null)
        {
            throw new ArgumentNullException(nameof(resultPayrolls));
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var existingResultPayrolls = await _context.ResultPayrolls.ToListAsync();
            _context.ResultPayrolls.RemoveRange(existingResultPayrolls);

            await _context.ResultPayrolls.AddRangeAsync(resultPayrolls);
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
