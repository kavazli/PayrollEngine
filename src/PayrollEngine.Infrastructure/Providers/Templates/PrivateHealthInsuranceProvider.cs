
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Providers;


namespace PayrollEngine.Infrastructure.Providers.Templates;


// Özel sağlık sigortası nesnelerinin veritabanında tutulması ve yönetilmesi için oluşturulan provider.
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


    // Veritabanına yeni bir PrivateHealthInsurance nesnesi ekler.
    // Eğer null bir nesne gönderilirse, ArgumentNullException fırlatır.
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


    // Veritabanına birden fazla PrivateHealthInsurance nesnesi ekler.
    // Eğer null veya boş bir liste gönderilirse, ArgumentException fırlatır.
    public async Task<List<PrivateHealthInsurance>> AddRangeAsync(List<PrivateHealthInsurance> healthInsuranceList)
    {
        if (healthInsuranceList == null || healthInsuranceList.Count == 0)
        {
            throw new ArgumentException("Health insurance list cannot be null or empty.", nameof(healthInsuranceList));
        }

        await _context.PrivateHealthInsurances.AddRangeAsync(healthInsuranceList);
        await _context.SaveChangesAsync();
        return healthInsuranceList;
    }


    // Veritabanındaki tüm PrivateHealthInsurance nesnelerini temizler.
    public async Task ClearAsync()
    {
        var healthInsurances = await _context.PrivateHealthInsurances.ToListAsync();
        _context.PrivateHealthInsurances.RemoveRange(healthInsurances);
        await _context.SaveChangesAsync();
    }


    // Veritabanındaki tüm PrivateHealthInsurance nesnelerini döndürür.
    // Eğer veritabanında hiç PrivateHealthInsurance yoksa, hata fırlatır.
    public Task<List<PrivateHealthInsurance>> GetAsync()
    {   

       var healthInsurances = _context.PrivateHealthInsurances.ToListAsync();
       if (healthInsurances == null)
       {
           throw new InvalidOperationException("No private health insurances found in the database.");
       } 

       return _context.PrivateHealthInsurances.ToListAsync(); 
       
    }



    // Veritabanındaki PrivateHealthInsurance nesnelerini temizler ve ardından,
    // yeni bir PrivateHealthInsurance nesnesi ekler.
    public async Task SetAsync(PrivateHealthInsurance healthInsurance)
    {   
        if (healthInsurance == null)
        {
            throw new ArgumentNullException(nameof(healthInsurance), "HealthInsurance cannot be null.");        
        }


        // Veritabanında birden fazla PrivateHealthInsurance nesnesi olabileceği için,
        // İşlem açıp kapatmak daha güvenli olacaktır. Böylece,
        // herhangi bir hata durumunda, veritabanı tutarsız kalmaz.
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Clear ve Add bir transaction içinde
            var healthInsurancesList = await _context.PrivateHealthInsurances.ToListAsync();
            if (healthInsurancesList == null || healthInsurancesList.Count == 0)
            {
                throw new InvalidOperationException("No private health insurances found in the database to clear.");
            }

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
