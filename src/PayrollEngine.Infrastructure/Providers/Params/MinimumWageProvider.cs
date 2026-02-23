
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;


namespace PayrollEngine.Infrastructure.Providers;


// Her yıl açıklanan asgari ücret değerlerini içeren tabloya erişim sağlayan provider. 
// Her yıl için tek bir kayıt bulunur ve yıl bazında sorgulanır. Eğer o yıl için kayıt yoksa hata fırlatır.
public class MinimumWageProvider : IMinimumWageProvider
{
    private readonly PayrollEngineDbContext _context;

    public MinimumWageProvider(PayrollEngineDbContext context)
    {
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
    }


    // DB tablosunda hangi yıl isteniyorsa o yılın verisini döndürüyor.
    // Eğer o yıl için kayıt yoksa hata fırlatıyor.
    public async Task<MinimumWage> GetValueAsync(int year)
    {
        var result = await _context.MinimumWages.SingleOrDefaultAsync(item => item.Year == year);

        if(result == null)
        {
            throw new InvalidOperationException($"MinimumWage not found for year {year}.");          
        }

        return result;
    }

}
