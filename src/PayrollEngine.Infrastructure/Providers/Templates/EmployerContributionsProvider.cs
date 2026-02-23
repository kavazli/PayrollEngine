using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Providers;


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


    public async Task<List<EmployerContributions>> AddRangeAsync(List<EmployerContributions> contributionsList)
    {
        if (contributionsList == null || contributionsList.Count == 0)
        {
            throw new ArgumentException("Contributions list cannot be null or empty.", nameof(contributionsList));
        }

        await _context.EmployerContributions.AddRangeAsync(contributionsList);
        await _context.SaveChangesAsync();
        return contributionsList;
    }


    public async Task ClearAsync()
    {
        var contributions = await _context.EmployerContributions.ToListAsync();
        _context.EmployerContributions.RemoveRange(contributions);
        await _context.SaveChangesAsync();
    }


    public Task<List<EmployerContributions>> GetAsync()
    {
        return _context.EmployerContributions.ToListAsync();
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
