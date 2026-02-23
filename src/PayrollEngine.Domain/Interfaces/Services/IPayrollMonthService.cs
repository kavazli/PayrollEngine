using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Services;

public interface IPayrollMonthService
{
    Task<List<PayrollMonth>> AddAsync(List<PayrollTemplateMonth> templateMonths, EmployeeScenario scenario);
    Task<List<PayrollMonth>> GetAsync();
    Task ClearAsync();
    Task SetAsync(List<PayrollMonth> months);
}
