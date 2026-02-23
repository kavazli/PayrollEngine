

using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces.Providers;


// İşveren katkılarını sağlayan provider arayüzü.
// Bu arayüz, işveren katkılarını eklemek, almak, 
// temizlemek ve belirli bir katkıyı güncellemek için yöntemler içerir.
public interface IEmployerContributionsProvider
{
    Task<EmployerContributions> AddAsync(EmployerContributions contributions);
    Task<List<EmployerContributions>> AddRangeAsync(List<EmployerContributions> contributionsList);
    Task<List<EmployerContributions>> GetAsync();
    Task ClearAsync();
    Task SetAsync(EmployerContributions contributions);  
}
