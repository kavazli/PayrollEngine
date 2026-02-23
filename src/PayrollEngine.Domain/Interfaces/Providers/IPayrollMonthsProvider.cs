using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Providers;

public interface IPayrollMonthsProvider
{
    Task<List<PayrollMonth>> AddAsync(List<PayrollMonth> months);
    Task<List<PayrollMonth>> GetAsync();
    Task ClearAsync();
    Task SetAsync(List<PayrollMonth> months);
}
