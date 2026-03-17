

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;


namespace PayrollEngine.Domain.Interfaces.Services;


// Alışveriş çeki bilgilerini yöneten servis arayüzü.
// Bu arayüz, alışveriş çeki bilgilerini eklemek, almak, temizlemek
// ve belirli bir alışveriş çekini güncellemek için yöntemler içerir.
public interface IShoppingVoucherService
{
    Task<ShoppingVoucher> AddAsync(ShoppingVoucher shoppingVoucher);
    Task<List<ShoppingVoucher>> AddRangeAsync(List<ShoppingVoucher> shoppingVoucherList);
    Task ClearAsync();
    Task<List<ShoppingVoucher>> GetAsync();
    Task<ShoppingVoucher> GetMonthAsync(Months months);
}
