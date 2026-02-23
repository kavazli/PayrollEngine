

using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces.Services;


// Çalışan senaryolarını yöneten servis arayüzü.
// Bu arayüz, çalışan senaryolarını eklemek, almak, temizlemek ve
// belirli bir senaryoyu güncellemek için yöntemler içerir.
public interface IEmployeeScenariosService
{
    Task<EmployeeScenario> AddAsync(EmployeeScenario scenario);
    Task<EmployeeScenario> GetAsync();
    Task ClearAsync();
    Task SetAsync(EmployeeScenario scenario);
}
