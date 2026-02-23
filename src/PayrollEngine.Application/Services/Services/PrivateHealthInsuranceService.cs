

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Services;
using PayrollEngine.Domain.Interfaces.Providers;


namespace PayrollEngine.Application.Services.Services;


// Bu sınıf, özel sağlık sigortası ile ilgili işlemleri gerçekleştiren servis sınıfıdır.
// Bu sınıf, IPrivateHealthInsuranceProvider arayüzünü kullanarak özel sağlık sigortasını
// eklemek, almak, temizlemek ve güncellemek için yöntemler sağlar.
public class PrivateHealthInsuranceService : IPrivateHealthInsuranceService
{   

    private readonly IPrivateHealthInsuranceProvider _healthInsuranceProvider;

    public PrivateHealthInsuranceService(IPrivateHealthInsuranceProvider healthInsuranceProvider)
    {   

        if (healthInsuranceProvider == null)
        {
            throw new ArgumentNullException(nameof(healthInsuranceProvider), "Health insurance provider cannot be null.");
        }

        _healthInsuranceProvider = healthInsuranceProvider;
    }


    // Özel sağlık sigortasını asenkron olarak ekler. 
    // Bu yöntem, geçerli bir sağlık sigortası sağlanmazsa bir ArgumentNullException fırlatır.
    public async Task<PrivateHealthInsurance> AddAsync(PrivateHealthInsurance privateHealthInsurance)
    {
        if (privateHealthInsurance == null)
        {
            throw new ArgumentNullException(nameof(privateHealthInsurance), "Private health insurance cannot be null.");
        }

        await _healthInsuranceProvider.AddAsync(privateHealthInsurance);
        return privateHealthInsurance;
    }


    // Birden fazla özel sağlık sigortasını asenkron olarak ekler.
    // Bu yöntem, geçerli bir sağlık sigortası listesi sağlanmazsa bir ArgumentNullException fırlatır.
    public async Task<List<PrivateHealthInsurance>> AddRangeAsync(List<PrivateHealthInsurance> privateHealthInsuranceList)
    {
        if(privateHealthInsuranceList == null)
        {
            throw new ArgumentNullException(nameof(privateHealthInsuranceList), "Private health insurance list cannot be null.");
        }

        await _healthInsuranceProvider.AddRangeAsync(privateHealthInsuranceList);
        return privateHealthInsuranceList;
    }


    // Özel sağlık sigortasını asenkron olarak temizler.
    public Task ClearAsync()
    {
        return _healthInsuranceProvider.ClearAsync();
    }

    // Özel sağlık sigortasını asenkron olarak alır.
    public Task<List<PrivateHealthInsurance>> GetAsync()
    {
        return _healthInsuranceProvider.GetAsync();
    }
}
