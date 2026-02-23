using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Providers;

public interface IShoppingVoucherProvider
{
    Task<ShoppingVoucher> AddAsync(ShoppingVoucher shoppingVoucher);
    Task<ShoppingVoucher> GetAsync();
    Task ClearAsync();
    Task SetAsync(ShoppingVoucher shoppingVoucher);
}
