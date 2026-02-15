using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Templates;

public interface IPayrollMonthsProvider
{
    Task<List<PayrollMonth>> AddAsync(List<PayrollMonth> months);
    Task<List<PayrollMonth>> GetAsync();
    Task ClearAsync();
    Task SetAsync(List<PayrollMonth> months);
}
