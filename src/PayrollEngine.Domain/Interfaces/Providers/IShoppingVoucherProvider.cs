using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Providers;

public interface IShoppingVoucherProvider
{
    Task<ShoppingVoucher> AddAsync(ShoppingVoucher shoppingVoucher);
    Task<List<ShoppingVoucher>> AddRangeAsync(List<ShoppingVoucher> shoppingVoucherList);
    Task<List<ShoppingVoucher>> GetAsync();
    Task ClearAsync();
    Task SetAsync(ShoppingVoucher shoppingVoucher);
}
