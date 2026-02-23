using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Services;
using PayrollEngine.Domain.Interfaces.Providers;

namespace PayrollEngine.Application.Services.Services;

public class ShoppingVoucherService : IShoppingVoucherService
{   

    private readonly IShoppingVoucherProvider _shoppingVoucherProvider;

    public ShoppingVoucherService(IShoppingVoucherProvider shoppingVoucherProvider)
    {
        if (shoppingVoucherProvider == null)
        {
            throw new ArgumentNullException(nameof(shoppingVoucherProvider), "Shopping voucher provider cannot be null.");
        }

        _shoppingVoucherProvider = shoppingVoucherProvider;
    }


    public async Task<ShoppingVoucher> AddAsync(ShoppingVoucher shoppingVoucher)
    {
        if (shoppingVoucher == null)
        {
            throw new ArgumentNullException(nameof(shoppingVoucher), "Shopping voucher cannot be null.");
        }

        await _shoppingVoucherProvider.AddAsync(shoppingVoucher);
        return shoppingVoucher;
    }


    public async Task<List<ShoppingVoucher>> AddRangeAsync(List<ShoppingVoucher> shoppingVoucherList)
    {
       if (shoppingVoucherList == null)
        {
            throw new ArgumentNullException(nameof(shoppingVoucherList), "Shopping voucher list cannot be null.");
        }

       await _shoppingVoucherProvider.AddRangeAsync(shoppingVoucherList);
       return shoppingVoucherList;
    }


    public Task ClearAsync()
    {
       return _shoppingVoucherProvider.ClearAsync();
    }
    

    public Task<List<ShoppingVoucher>> GetAsync()
    {
        return _shoppingVoucherProvider.GetAsync();
    }
}
