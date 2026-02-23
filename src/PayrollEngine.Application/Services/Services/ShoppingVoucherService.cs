

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Services;
using PayrollEngine.Domain.Interfaces.Providers;


namespace PayrollEngine.Application.Services.Services;


// Bu sınıf, alışveriş çeki ile ilgili işlemleri gerçekleştiren servis sınıfıdır.
// Bu sınıf, IShoppingVoucherProvider arayüzünü kullanarak alışveriş çeki bilgilerini
// eklemek, almak, temizlemek ve güncellemek için yöntemler sağlar.
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


    // Alışveriş çeki bilgilerini asenkron olarak ekler. 
    // Bu yöntem, geçerli bir alışveriş çeki sağlanmazsa bir Argument
    public async Task<ShoppingVoucher> AddAsync(ShoppingVoucher shoppingVoucher)
    {
        if (shoppingVoucher == null)
        {
            throw new ArgumentNullException(nameof(shoppingVoucher), "Shopping voucher cannot be null.");
        }

        await _shoppingVoucherProvider.AddAsync(shoppingVoucher);
        return shoppingVoucher;
    }


    // Birden fazla alışveriş çeki bilgisini asenkron olarak ekler.
    // Bu yöntem, geçerli bir alışveriş çeki listesi sağlanmazsa bir ArgumentNullException fırlatır.
    public async Task<List<ShoppingVoucher>> AddRangeAsync(List<ShoppingVoucher> shoppingVoucherList)
    {
       if (shoppingVoucherList == null)
        {
            throw new ArgumentNullException(nameof(shoppingVoucherList), "Shopping voucher list cannot be null.");
        }

       await _shoppingVoucherProvider.AddRangeAsync(shoppingVoucherList);
       return shoppingVoucherList;
    }


    // Alışveriş çeki bilgilerini asenkron olarak temizler.
    public Task ClearAsync()
    {
       return _shoppingVoucherProvider.ClearAsync();
    }
    

    // Alışveriş çeki bilgilerini asenkron olarak alır.
    public Task<List<ShoppingVoucher>> GetAsync()
    {
        return _shoppingVoucherProvider.GetAsync();
    }
}
