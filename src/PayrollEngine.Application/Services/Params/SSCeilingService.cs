

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;


namespace PayrollEngine.Application.Services.Params;


// Sosyal güvenlik tavan bilgisini sağlayan servis sınıfı.
// Bu sınıf, sosyal güvenlik tavan bilgisini almak için bir yöntem içerir.
public class SSCeilingService
{

    private readonly ISSCeilingProvider _ssCeilingProvider;


    public SSCeilingService(ISSCeilingProvider ssCeilingProvider)
    {
        if (ssCeilingProvider == null)
        {
            throw new ArgumentNullException(nameof(ssCeilingProvider));
        }
        _ssCeilingProvider = ssCeilingProvider;
    }


    // Sosyal güvenlik tavan bilgisini almak için kullanılan yöntem.
    // Bu yöntem, geçersiz bir yıl değeri sağlanması durumunda bir ArgumentOutOfRangeException fırlatır.
    public async Task<SSCeiling> GetValueAsync(int year)
    {   
        if(year < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(year), "Year cannot be negative.");
        }

        return await _ssCeilingProvider.GetValueAsync(year);
    }

}

