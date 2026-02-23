

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;


namespace PayrollEngine.Application.Services.Params;


// Damga vergisi bilgisini sağlayan servis sınıfı.
// Bu sınıf, damga vergisi bilgisini almak için bir yöntem içerir.
public class StampTaxService
{

    private readonly IStampTaxProvider _stampTaxProvider;


    public StampTaxService(IStampTaxProvider stampTaxProvider)
    {   
        if (stampTaxProvider == null)
        {
            throw new ArgumentNullException(nameof(stampTaxProvider));
        }
        _stampTaxProvider = stampTaxProvider;
    }

    // Damga vergisi bilgisini almak için kullanılan yöntem.
    // Bu yöntem, geçersiz bir yıl değeri sağlanması durumunda bir ArgumentOutOfRangeException fırlatır.
    public async Task<StampTax> GetValueAsync(int year)
    {   
        if(year < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(year), "Year cannot be negative.");
        }
        return await _stampTaxProvider.GetValueAsync(year);
    }
}
