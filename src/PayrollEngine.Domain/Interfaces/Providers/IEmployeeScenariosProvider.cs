

using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces.Providers;


// Çalışan senaryolarını sağlayan provider arayüzü.
// Bu arayüz, çalışan senaryolarını eklemek, almak, temizlemek ve
public interface IEmployeeScenariosProvider
{
    Task<EmployeeScenario> AddAsync(EmployeeScenario scenario);
    Task<EmployeeScenario> GetAsync();
    Task ClearAsync();
    Task SetAsync(EmployeeScenario scenario);   
}
