
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces;


namespace PayrollEngine.Infrastructure.Providers;


// Çalışanın her ay için toplam gelir vergisi matrahını içeren tabloya erişim sağlayan provider.
// Her ay için tek bir kayıt bulunur ve ay bazında sorgulanır. Eğer o ay için kayıt yoksa hata fırlatır.
public class CumulativeIncomeTaxBaseProvider : ICumulativeIncomeTaxBaseProvider
{   

    private readonly PayrollEngineDbContext _context;

    public CumulativeIncomeTaxBaseProvider(PayrollEngineDbContext context)
    {   
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
        
    }


    // DB tablosunda hangi ay isteniyorsa o aya ait veriyi döndürüyor. 
    // Eğer o ay için kayıt yoksa hata fırlatıyor.
    public async Task<CumulativeIncomeTaxBase> GetValueAsync(Months month)
    {
        var result = await _context.CumulativeIncomeTaxBases.SingleOrDefaultAsync(item => item.Month == month);
        
        if(result == null)
        {
            throw new InvalidOperationException($"CumulativeIncomeTaxBase not found for month {month}.");          
        }

        return result;
    }


    // CumulativeIncomeTaxBase tablosuna yeni bir kayıt eklemek için kullanılan method. 
    // Genellikle her ayın sonunda, o aya ait toplam gelir vergisi matrahını kaydetmek için kullanılır.
    public async Task<CumulativeIncomeTaxBase> AddAsync(CumulativeIncomeTaxBase taxBase)
    {
        if (taxBase == null)
        {
            throw new ArgumentNullException(nameof(taxBase));
        }

        _context.CumulativeIncomeTaxBases.Add(taxBase);
        await _context.SaveChangesAsync();

        return taxBase;
    }


    // CumulativeIncomeTaxBase tablosundaki tüm kayıtları silmek için kullanılan method.
    // Genellikle her yıl sonunda, yeni yılın başında eski kayıtları temizlemek için kullanılır.
    public async Task ClearAsync()
    {
        var taxBases = await _context.CumulativeIncomeTaxBases.ToListAsync();
        _context.CumulativeIncomeTaxBases.RemoveRange(taxBases);
        await _context.SaveChangesAsync();
    }

    
}
