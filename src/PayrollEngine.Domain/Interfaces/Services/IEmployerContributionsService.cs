

using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces.Services;


// İşveren katkılarını yöneten servis arayüzü.
// Bu arayüz, işveren katkılarını eklemek, almak, temizlemek ve
// belirli bir katkıyı güncellemek için yöntemler içerir.
public interface IEmployerContributionsService
{
    Task<EmployerContributions> AddAsync(EmployerContributions employerContributions);
    Task<List<EmployerContributions>> AddRangeAsync(List<EmployerContributions> employerContributionsList);
    Task ClearAsync();
    Task<List<EmployerContributions>> GetAsync();
}
