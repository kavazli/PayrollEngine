

using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces.Services;


// Özel sağlık sigortası bilgilerini yöneten servis arayüzü.
// Bu arayüz, özel sağlık sigortası bilgilerini eklemek, almak, temizlemek
// ve belirli bir sağlık sigortası bilgisini güncellemek için yöntemler içerir.
public interface IPrivateHealthInsuranceService
{
    Task<PrivateHealthInsurance> AddAsync(PrivateHealthInsurance privateHealthInsurance);
    Task<List<PrivateHealthInsurance>> AddRangeAsync(List<PrivateHealthInsurance> privateHealthInsuranceList);
    Task ClearAsync();
    Task<List<PrivateHealthInsurance>> GetAsync();
}
