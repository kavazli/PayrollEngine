using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Services;

public interface IShoppingVoucherService
{
    Task<ShoppingVoucher> AddAsync(ShoppingVoucher shoppingVoucher);
    Task<List<ShoppingVoucher>> AddRangeAsync(List<ShoppingVoucher> shoppingVoucherList);
    Task ClearAsync();
    Task<List<ShoppingVoucher>> GetAsync();
}
