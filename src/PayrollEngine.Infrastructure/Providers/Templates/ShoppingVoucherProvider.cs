using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Providers;


namespace PayrollEngine.Infrastructure.Providers.Templates;

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


    public async Task ClearAsync()
    {
        var shoppingVouchers = await _context.ShoppingVouchers.ToListAsync();
        _context.ShoppingVouchers.RemoveRange(shoppingVouchers);
        await _context.SaveChangesAsync();    
    }


    public Task<List<ShoppingVoucher>> GetAsync()
    {
       return _context.ShoppingVouchers.ToListAsync();  
    }

    public async Task SetAsync(ShoppingVoucher shoppingVoucher)
    {
        if (shoppingVoucher == null)
        {
            throw new ArgumentNullException(nameof(shoppingVoucher), "ShoppingVoucher cannot be null.");
        }

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {  
            try
            {
                // Clear ve Add bir transaction içinde
                var shoppingVouchersList = await _context.ShoppingVouchers.ToListAsync();
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
