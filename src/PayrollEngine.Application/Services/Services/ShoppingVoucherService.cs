using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Services;

namespace PayrollEngine.Application.Services.Services;

public class ShoppingVoucherService : IShoppingVoucherService
{
    public Task<ShoppingVoucher> AddAsync(ShoppingVoucher shoppingVoucher)
    {
        throw new NotImplementedException();
    }

    public Task<List<ShoppingVoucher>> AddRangeAsync(List<ShoppingVoucher> shoppingVoucherList)
    {
        throw new NotImplementedException();
    }

    public Task ClearAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<ShoppingVoucher>> GetAsync()
    {
        throw new NotImplementedException();
    }
}
