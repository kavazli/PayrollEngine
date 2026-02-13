using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Providers.Templates;

public class EmployeeScenariosProvider
{
    readonly PayrollEngineDbContext _Context;

    public EmployeeScenariosProvider(PayrollEngineDbContext Context)
    {   
        if (Context == null)
        {
            throw new ArgumentNullException(nameof(Context));
        }

        _Context = Context;
    }


   public async Task<EmployeeScenario> AddAsync(EmployeeScenario scenario)
    {
    if (scenario == null)
    {
        throw new ArgumentNullException(nameof(scenario));
    }
        

    _Context.EmployeeScenarios.Add(scenario);
    await _Context.SaveChangesAsync();
    return scenario;

    }


    public async Task<EmployeeScenario> GetAsync()
    {
        return await _Context.EmployeeScenarios.FirstOrDefaultAsync();
    }



    public async Task ClearAsync()
    {
        var scenarios = await _Context.EmployeeScenarios.ToListAsync();
        _Context.EmployeeScenarios.RemoveRange(scenarios);
        await _Context.SaveChangesAsync();
    }


    public async Task SetAsync(EmployeeScenario scenario)
{
    if (scenario == null)
    {
        throw new ArgumentNullException(nameof(scenario));

    }
        
    using (var transaction = _Context.Database.BeginTransaction())
    {
        try
        {
            // Clear ve Add bir transaction içinde
            var scenarios = await _Context.EmployeeScenarios.ToListAsync();
            _Context.EmployeeScenarios.RemoveRange(scenarios);
            _Context.EmployeeScenarios.Add(scenario);
            
            await _Context.SaveChangesAsync();
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
