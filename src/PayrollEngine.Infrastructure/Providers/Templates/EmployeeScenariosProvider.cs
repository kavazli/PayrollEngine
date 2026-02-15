using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Templates;

namespace PayrollEngine.Infrastructure.Providers.Templates;

public class EmployeeScenariosProvider : IEmployeeScenariosProvider
{
    private readonly PayrollEngineDbContext _context;

    public EmployeeScenariosProvider(PayrollEngineDbContext context)
    {   
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        _context = context;
    }


   public async Task<EmployeeScenario> AddAsync(EmployeeScenario scenario)
    {
        if (scenario == null)
        {
            throw new ArgumentNullException(nameof(scenario));
        }
            

        _context.EmployeeScenarios.Add(scenario);
        await _context.SaveChangesAsync();
        return scenario;

    }


    public async Task<EmployeeScenario> GetAsync()
    {
        return await _context.EmployeeScenarios.FirstOrDefaultAsync();
    }



    public async Task ClearAsync()
    {
        var scenarios = await _context.EmployeeScenarios.ToListAsync();
        _context.EmployeeScenarios.RemoveRange(scenarios);
        await _context.SaveChangesAsync();
    }


    public async Task SetAsync(EmployeeScenario scenario)
    {
        if (scenario == null)
        {
            throw new ArgumentNullException(nameof(scenario));

        }
            
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                // Clear ve Add bir transaction içinde
                var scenarios = await _context.EmployeeScenarios.ToListAsync();
                _context.EmployeeScenarios.RemoveRange(scenarios);
                _context.EmployeeScenarios.Add(scenario);
                
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

}
