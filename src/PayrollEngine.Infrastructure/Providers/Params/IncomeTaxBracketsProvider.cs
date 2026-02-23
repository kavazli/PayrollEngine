
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;


namespace PayrollEngine.Infrastructure.Providers;


// Her yıl açıklanan gelir vergisi dilimlerini içeren tabloya erişim sağlayan provider. 
// Her yıl için birden fazla kayıt bulunur ve yıl bazında sorgulanır. Eğer o yıl için hiç kayıt yoksa hata fırlatır.
public class IncomeTaxBracketsProvider : IIncomeTaxBracketsProvider
{
    private readonly PayrollEngineDbContext _context;

    public IncomeTaxBracketsProvider(PayrollEngineDbContext context)
    {
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
    }


    // DB tablosunda hangi yıl isteniyorsa o yılın verilerini döndürüyor. 
    // Eğer o yıl için hiç kayıt yoksa hata fırlatıyor.
    public async Task<List<IncomeTaxBracket>> GetValueAsync(int year)
    {
        var result = await _context.IncomeTaxBrackets.Where(b => b.Year == year).ToListAsync();

        if(result == null || result.Count == 0)
        {
            throw new InvalidOperationException($"No IncomeTaxBrackets found for year {year}.");          
        }

        return result.OrderBy(b => b.MinAmount).ToList();
    }

}
