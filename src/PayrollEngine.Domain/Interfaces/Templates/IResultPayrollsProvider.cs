using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Templates;

public interface IResultPayrollsProvider
{
    Task<List<ResultPayroll>> AddAsync(List<ResultPayroll> resultPayrolls);
    Task<List<ResultPayroll>> GetAsync();
    Task ClearAsync();
    Task SetAsync(List<ResultPayroll> resultPayrolls);
}
