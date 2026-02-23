
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Providers;


namespace PayrollEngine.Infrastructure.Providers.Templates;


// UI tarafında kullanıcı tarafından gönderilen EmployeeScenario nesnesini veritabanında,
// saklamak ve yönetmek için kullanılan provider sınıfı.
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


   // EmployeeScenario nesnesini veritabanına ekler. 
   // Eğer null bir nesne gönderilirse, ArgumentNullException fırlatır.
   public async Task<EmployeeScenario> AddAsync(EmployeeScenario scenario)
    {
        if (scenario == null)
        {
            throw new ArgumentNullException(nameof(scenario));
        }
            
        await _context.EmployeeScenarios.AddAsync(scenario);
        await _context.SaveChangesAsync();
        return scenario;
    }


    // Veritabanındaki ilk EmployeeScenario nesnesini döndürür.
    // Eğer veritabanında hiç EmployeeScenario yoksa, hata fırlatır.
    public async Task<EmployeeScenario> GetAsync()
    {   
        var scenarios = await _context.EmployeeScenarios.FirstOrDefaultAsync();
        if (scenarios == null)
        {
            throw new InvalidOperationException("No employee scenarios found in the database.");
        }

        return scenarios;
    }

    // Veritabanındaki tüm EmployeeScenario nesnelerini temizler.
    public async Task ClearAsync()
    {
        var scenarios = await _context.EmployeeScenarios.ToListAsync();
        _context.EmployeeScenarios.RemoveRange(scenarios);
        await _context.SaveChangesAsync();
    }


    // Veritabanındaki EmployeeScenario nesnelerini temizler ve ardından,
    // yeni bir EmployeeScenario nesnesi ekler.
    public async Task SetAsync(EmployeeScenario scenario)
    {
        if (scenario == null)
        {
            throw new ArgumentNullException(nameof(scenario), "EmployeeScenario cannot be null.");

        }

        // Veritabanında birden fazla EmployeeScenario nesnesi olabileceği için, 
        // İşlem açıp kapatmak daha güvenli olacaktır. Böylece, 
        // herhangi bir hata durumunda, veritabanı tutarsız kalmaz.   
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Clear ve Add bir transaction içinde
            var scenarios = await _context.EmployeeScenarios.ToListAsync();
            if(scenarios == null || scenarios.Count == 0)
            {
                throw new InvalidOperationException("No employee scenarios found in the database to clear.");
            }

            _context.EmployeeScenarios.RemoveRange(scenarios);
            await _context.EmployeeScenarios.AddAsync(scenario);
            
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
