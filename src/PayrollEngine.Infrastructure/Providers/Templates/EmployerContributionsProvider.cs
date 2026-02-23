
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Providers;


namespace PayrollEngine.Infrastructure.Providers.Templates;


// Sgk İşveren paylarınına işlenmesi ve işveren maliyetinin tabloda tutulması için oluşturulan provider.
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


    // Veritabanına yeni bir EmployerContributions nesnesi ekler.
    // Eğer null bir nesne gönderilirse, ArgumentNullException fırlatır.
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


    // Veritabanına birden fazla EmployerContributions nesnesi ekler.
    // Eğer null veya boş bir liste gönderilirse, ArgumentException fırlatır.
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


    // Veritabanındaki tüm EmployerContributions nesnelerini temizler.
    public async Task ClearAsync()
    {
        var contributions = await _context.EmployerContributions.ToListAsync();
        _context.EmployerContributions.RemoveRange(contributions);
        await _context.SaveChangesAsync();
    }


    // Veritabanındaki tüm EmployerContributions nesnelerini döndürür.
    // Eğer veritabanında hiç EmployerContributions yoksa, hata fırlatır.
    public Task<List<EmployerContributions>> GetAsync()
    {   
        var contributions = _context.EmployerContributions.ToListAsync();
        if (contributions == null )
        {
            throw new InvalidOperationException("No employer contributions found in the database.");
        }

        return contributions;
    }


    // Veritabanındaki EmployerContributions nesnelerini temizler ve ardından,
    // yeni bir EmployerContributions nesnesi ekler.
    public async Task SetAsync(EmployerContributions contributions)
    {   
        if (contributions == null)
        {
            throw new ArgumentNullException(nameof(contributions), "EmployerContributions cannot be null.");        
        }

        // Veritabanında birden fazla EmployerContributions nesnesi olabileceği için,
        // İşlem açıp kapatmak daha güvenli olacaktır. Böylece, 
        // herhangi bir hata durumunda, veritabanı tutarsız kalmaz.
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Clear ve Add bir transaction içinde
            var contributionsList = await _context.EmployerContributions.ToListAsync();
            if(contributionsList == null || contributionsList.Count == 0)
            {
                throw new InvalidOperationException("No employer contributions found in the database to clear.");
            }

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
