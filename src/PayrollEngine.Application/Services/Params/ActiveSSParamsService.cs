

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;


namespace PayrollEngine.Application.Services.Params;


// Normal Çalışanların Sosyal Sigorta Parametrelerini sağlayan servis sınıfı.
// Bu sınıf, IActiveSSParamsProvider arayüzünü kullanarak belirli bir
// yıl için aktif sosyal sigorta parametrelerini almak için bir yöntem sağlar.
public class ActiveSSParamsService
{

    private readonly IActiveSSParamsProvider _activeSSParamsProvider;


    public ActiveSSParamsService(IActiveSSParamsProvider activeSSParamsProvider)
    {
        if (activeSSParamsProvider == null)
        {
            throw new ArgumentNullException(nameof(activeSSParamsProvider));
        }
        _activeSSParamsProvider = activeSSParamsProvider;   
    }


    // Belirli bir yıl için aktif sosyal sigorta parametrelerini asenkron olarak alır.
    // Bu yöntem, geçerli bir yıl sağlanmazsa bir ArgumentOutOfRangeException fırlatır.
    public Task<ActiveSSParams> GetValueAsync(int year)
    {   
        if(year <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(year), "Year must be a positive integer.");
        }    

        return _activeSSParamsProvider.GetValueAsync(year);
    }

}
