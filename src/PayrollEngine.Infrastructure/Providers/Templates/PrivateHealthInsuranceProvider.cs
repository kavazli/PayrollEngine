using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Templates;

namespace PayrollEngine.Infrastructure.Providers.Templates;

public class PrivateHealthInsuranceProvider : IPrivateHealthInsuranceProvider
{   

    private readonly PayrollEngineDbContext _context;

    public PrivateHealthInsuranceProvider(PayrollEngineDbContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context), "PayrollEngineDbContext cannot be null.");
        }

        _context = context;
    }


    public async Task<PrivateHealthInsurance> AddAsync(PrivateHealthInsurance healthInsurance)
    {
        if (healthInsurance == null)
        {
            throw new ArgumentNullException(nameof(healthInsurance), "PrivateHealthInsurance cannot be null.");
        }

        await _context.PrivateHealthInsurances.AddAsync(healthInsurance);
        await _context.SaveChangesAsync();
        return healthInsurance;       
    }


    public async Task ClearAsync()
    {
        var healthInsurances = await _context.PrivateHealthInsurances.ToListAsync();
        _context.PrivateHealthInsurances.RemoveRange(healthInsurances);
        await _context.SaveChangesAsync();
    }


    public Task<PrivateHealthInsurance> GetAsync()
    {
       return _context.PrivateHealthInsurances.FirstOrDefaultAsync(); 
       
    }


    public async Task SetAsync(PrivateHealthInsurance healthInsurance)
    {   
        if (healthInsurance == null)
        {
            throw new ArgumentNullException(nameof(healthInsurance), "HealthInsurance cannot be null.");        
        }

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Clear ve Add bir transaction içinde
            var healthInsurancesList = await _context.PrivateHealthInsurances.ToListAsync();
            _context.PrivateHealthInsurances.RemoveRange(healthInsurancesList);
            await _context.PrivateHealthInsurances.AddAsync(healthInsurance);
            
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
