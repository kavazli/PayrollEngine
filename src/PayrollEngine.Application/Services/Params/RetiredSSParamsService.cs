

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;


namespace PayrollEngine.Application.Services.Params;


// Emekli sosyal güvenlik parametrelerini sağlayan servis sınıfı.
// Bu sınıf, emekli sosyal güvenlik parametrelerini almak için bir yöntem içer
public class RetiredSSParamsService
{
    
    private readonly IRetiredSSParamsProvider _retiredSSParamsProvider;


    public RetiredSSParamsService(IRetiredSSParamsProvider retiredSSParamsProvider)
    {
        if (retiredSSParamsProvider == null)
        {
            throw new ArgumentNullException(nameof(retiredSSParamsProvider));
        }
        _retiredSSParamsProvider = retiredSSParamsProvider;
    }


    // Emekli sosyal güvenlik parametrelerini almak için kullanılan yöntem.
    public async Task<RetiredSSParams> GetValueAsync(int year)
    {
        return await _retiredSSParamsProvider.GetValueAsync(year);
    }

}
