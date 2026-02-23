

using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces.Providers;


// Özel sağlık sigortasını sağlayan provider arayüzü.
// Bu arayüz, özel sağlık sigortasını eklemek, almak, temizlemek ve
// belirli bir sağlık sigortasını güncellemek için yöntemler içerir.
public interface IPrivateHealthInsuranceProvider
{
    Task<PrivateHealthInsurance> AddAsync(PrivateHealthInsurance healthInsurance);
    Task<List<PrivateHealthInsurance>> AddRangeAsync(List<PrivateHealthInsurance> healthInsuranceList);
    Task<List<PrivateHealthInsurance>> GetAsync();
    Task ClearAsync();
    Task SetAsync(PrivateHealthInsurance healthInsurance);
}
