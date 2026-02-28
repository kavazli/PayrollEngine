

using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Params;


namespace PayrollEngine.Infrastructure.Providers.Params;


// Engelli SGK ya tabi çalışanların, engellilik derecesi değerlerini içeren tabloya erişim sağlayan provider. 
// Her yıl için tek bir kayıt bulunur ve yıl bazında sorgulanır.
public class DisabilityDegreeProvider : IDisabilityDegreeProvider
{   

    private readonly PayrollEngineDbContext _context;

    public DisabilityDegreeProvider(PayrollEngineDbContext context)
    {   

        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
        
    }

    // DB tablosunda hangi yıl isteniyorsa o yılın verisini döndürüyor.
    // Eğer o yıl için kayıt yoksa hata fırlatıyor.
    public async Task<List<DisabilityDegree>> GetValueAsync(int year)
    {   
        var result = await _context.DisabilityDegrees.Where(item => item.Year == year).ToListAsync();

        if(result == null || result.Count == 0)
        {
            throw new InvalidOperationException($"DisabilityDegree not found for year {year}.");          
        }
        return result;
    }
}
