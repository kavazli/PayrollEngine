using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Services;

public interface IEmployeeScenariosService
{
    Task<EmployeeScenario> AddAsync(EmployeeScenario scenario);
    Task<EmployeeScenario> GetAsync();
    Task ClearAsync();
    Task SetAsync(EmployeeScenario scenario);
}
