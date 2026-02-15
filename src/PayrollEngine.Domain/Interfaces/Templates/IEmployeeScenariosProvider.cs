using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Templates;

public interface IEmployeeScenariosProvider
{
    Task<EmployeeScenario> AddAsync(EmployeeScenario scenario);
    Task<EmployeeScenario> GetAsync();
    Task ClearAsync();
    Task SetAsync(EmployeeScenario scenario);   
}
