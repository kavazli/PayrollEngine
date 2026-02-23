
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;


namespace PayrollEngine.Infrastructure.Providers;


// Normal SGK ya tabi çalışanların, Sgk işçi payıdeğerlerini içeren tabloya erişim sağlayan provider. 
// Her yıl için tek bir kayıt bulunur ve yıl bazında sorgulanır.
public class ActiveSSParamsProvider : IActiveSSParamsProvider
{   
    private readonly PayrollEngineDbContext _context;

    public ActiveSSParamsProvider(PayrollEngineDbContext context)
    {   
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;    
    }


    // DB tablosunda hangi yıl isteniyorsa o yılın verisini döndürüyor.
    // Eğer o yıl için kayıt yoksa hata fırlatıyor.    
    public async Task<ActiveSSParams> GetValueAsync(int year)
    {   
        var result = await _context.ActiveSSParams.SingleOrDefaultAsync(item => item.Year == year);

        if(result == null)
        {
            throw new InvalidOperationException($"ActiveSSParams not found for year {year}.");          
        }

        return result;
        
    }
}
