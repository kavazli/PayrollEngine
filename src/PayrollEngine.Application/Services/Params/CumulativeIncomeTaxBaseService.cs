


using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces;


namespace PayrollEngine.Application.Services.Params;


// Gelir Vergisi Matrahı Hesaplama için gerekli parametreleri sağlayan servis sınıfı.
// Bu sınıf, ICumulativeIncomeTaxBaseProvider arayüzünü kullanarak belirli bir 
//ay için gelir vergisi matrahı hesaplama parametrelerini almak, eklemek ve temizlemek için yöntemler sağlar.
public class CumulativeIncomeTaxBaseService
{

    private readonly ICumulativeIncomeTaxBaseProvider _cumulativeIncomeTaxBaseProvider;


    public CumulativeIncomeTaxBaseService(ICumulativeIncomeTaxBaseProvider cumulativeIncomeTaxBaseProvider)
    {
        if (cumulativeIncomeTaxBaseProvider == null)
        {
            throw new ArgumentNullException(nameof(cumulativeIncomeTaxBaseProvider));
        }
        _cumulativeIncomeTaxBaseProvider = cumulativeIncomeTaxBaseProvider;
    }


    // Belirli bir ay için gelir vergisi matrahı hesaplama parametrelerini asenkron olarak alır.
    // Bu yöntem, geçerli bir ay sağlanmazsa bir ArgumentOutOfRangeException fırlatır.
    public Task<CumulativeIncomeTaxBase> GetValueAsync(Months month)
    {   
        if(month < Months.January || month > Months.December)
        {
            throw new ArgumentOutOfRangeException(nameof(month), "Month must be a valid month of the year.");
        }

        return _cumulativeIncomeTaxBaseProvider.GetValueAsync(month);
    }


    // Gelir vergisi matrahı hesaplama parametrelerini asenkron olarak ekler.
    public Task<CumulativeIncomeTaxBase> AddAsync(CumulativeIncomeTaxBase taxBase)
    {
        return _cumulativeIncomeTaxBaseProvider.AddAsync(taxBase);
    }

    // Gelir vergisi matrahı hesaplama parametrelerini asenkron olarak temizler.
    public Task ClearAsync()
    {
        return _cumulativeIncomeTaxBaseProvider.ClearAsync();
    }
    
}
