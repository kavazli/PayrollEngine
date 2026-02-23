using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Templates;

namespace PayrollEngine.Infrastructure.Providers.Templates;

public class EmployerContributionsProvider : IEmployerContributionsProvider
{

    private readonly PayrollEngineDbContext _context;

    public EmployerContributionsProvider(PayrollEngineDbContext context)
    {   
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context), "PayrollEngineDbContext cannot be null.");
        }

        _context = context;
    }


    public async Task<EmployerContributions> AddAsync(EmployerContributions contributions)
    {
        if( contributions == null)
        {
            throw new ArgumentNullException(nameof(contributions), "EmployerContributions cannot be null.");
        }

        await _context.EmployerContributions.AddAsync(contributions);
        await _context.SaveChangesAsync();
        return contributions; 
    }


    public async Task ClearAsync()
    {
        var contributions = await _context.EmployerContributions.ToListAsync();
        _context.EmployerContributions.RemoveRange(contributions);
        await _context.SaveChangesAsync();
    }


    public Task<EmployerContributions> GetAsync()
    {
        return _context.EmployerContributions.FirstOrDefaultAsync();
    }


    public async Task SetAsync(EmployerContributions contributions)
    {   
        if (contributions == null)
        {
            throw new ArgumentNullException(nameof(contributions), "EmployerContributions cannot be null.");        
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Clear ve Add bir transaction içinde
            var contributionsList = await _context.EmployerContributions.ToListAsync();
            _context.EmployerContributions.RemoveRange(contributionsList);
            await _context.EmployerContributions.AddAsync(contributions);
            
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
