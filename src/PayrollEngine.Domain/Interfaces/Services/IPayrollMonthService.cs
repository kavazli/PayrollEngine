

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;


namespace PayrollEngine.Domain.Interfaces.Services;


// Bordro aylarını yöneten servis arayüzü.
// Bu arayüz, bordro aylarını eklemek, almak, temizlemek ve
// belirli bir bordro ayını güncellemek için yöntemler içerir.
public interface IPayrollMonthService
{
    Task<List<PayrollMonth>> AddAsync(List<PayrollTemplateMonth> templateMonths, EmployeeScenario scenario);
    Task<List<PayrollMonth>> GetAsync();
    Task<PayrollMonth> GetMonthAsync(Months month);
    Task ClearAsync();
    Task SetAsync(List<PayrollMonth> months);
}
