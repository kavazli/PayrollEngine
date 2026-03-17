
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces.Providers;


namespace PayrollEngine.Infrastructure.Providers.Templates;

// Alışveriş çeki nesnelerinin veritabanında tutulması ve yönetilmesi için oluşturulan provider.
public class ShoppingVoucherProvider : IShoppingVoucherProvider
{   

    private readonly PayrollEngineDbContext _context;

    public ShoppingVoucherProvider(PayrollEngineDbContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context), "PayrollEngineDbContext cannot be null.");
        }

        _context = context;
    }


    // Veritabanına yeni bir ShoppingVoucher nesnesi ekler.
    // Eğer null bir nesne gönderilirse, ArgumentNullException fırlatır.
    public async Task<ShoppingVoucher> AddAsync(ShoppingVoucher shoppingVoucher)
    {
        
        if(shoppingVoucher == null)
        {
            throw new ArgumentNullException(nameof(shoppingVoucher), "ShoppingVoucher cannot be null.");
        }

        await _context.ShoppingVouchers.AddAsync(shoppingVoucher);
        await _context.SaveChangesAsync();
        return shoppingVoucher;
    }


    // Veritabanına birden fazla ShoppingVoucher nesnesi ekler.
    // Eğer null veya boş bir liste gönderilirse, ArgumentException fırlatır.
    public async Task<List<ShoppingVoucher>> AddRangeAsync(List<ShoppingVoucher> shoppingVoucherList)
    {
        if (shoppingVoucherList == null || shoppingVoucherList.Count == 0)
        {
            throw new ArgumentException("Shopping voucher list cannot be null or empty.", nameof(shoppingVoucherList));
        }

        await _context.ShoppingVouchers.AddRangeAsync(shoppingVoucherList);
        await _context.SaveChangesAsync();
        return shoppingVoucherList;
    }


    // Veritabanındaki tüm ShoppingVoucher nesnelerini temizler.
    public async Task ClearAsync()
    {
        var shoppingVouchers = await _context.ShoppingVouchers.ToListAsync();
        _context.ShoppingVouchers.RemoveRange(shoppingVouchers);
        await _context.SaveChangesAsync();    
    }


    // Veritabanındaki tüm ShoppingVoucher nesnelerini döndürür.
    // Eğer veritabanında hiç ShoppingVoucher yoksa, hata fırlatır.
    public async Task<List<ShoppingVoucher>> GetAsync()
    {   
       var shoppingVouchers = await _context.ShoppingVouchers.ToListAsync();
        
       return shoppingVouchers;  
    }


    public async Task<ShoppingVoucher> GetMonthAsync(Months months)
    {
        var shoppingVoucher = await _context.ShoppingVouchers.FirstOrDefaultAsync(sv => sv.Month == months);
        if (shoppingVoucher == null)       
        {
            throw new InvalidOperationException($"No shopping voucher found for the month: {months}.");
        }

       return shoppingVoucher;  
    }

    // Veritabanındaki ShoppingVoucher nesnelerini temizler ve ardından,
    // yeni bir ShoppingVoucher nesnesi ekler.    
    public async Task SetAsync(ShoppingVoucher shoppingVoucher)
    {
        if (shoppingVoucher == null)
        {
            throw new ArgumentNullException(nameof(shoppingVoucher), "ShoppingVoucher cannot be null.");
        }

        // Veritabanında birden fazla ShoppingVoucher nesnesi olabileceği için,
        // işlem açıp kapatmak daha güvenli olacaktır. Böylece,
        // herhangi bir hata durumunda, veritabanı tutarsız kalmaz.
        using (var transaction = await _context.Database.BeginTransactionAsync())
        {  
            try
            {
                // Clear ve Add bir transaction içinde
                var shoppingVouchersList = await _context.ShoppingVouchers.ToListAsync();
                if(shoppingVouchersList == null || shoppingVouchersList.Count == 0)
                {
                    throw new InvalidOperationException("No shopping vouchers found in the database to clear.");
                }

                _context.ShoppingVouchers.RemoveRange(shoppingVouchersList);
                await _context.ShoppingVouchers.AddAsync(shoppingVoucher);
                
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
