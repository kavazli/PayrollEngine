using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Providers;

public interface IResultPayrollsProvider
{   
    Task<ResultPayroll> AddAsync(ResultPayroll resultPayroll);
    Task<List<ResultPayroll>> AddRangeAsync(List<ResultPayroll> resultPayrolls);
    Task<List<ResultPayroll>> GetAsync();
    Task ClearAsync();
    Task SetAsync(List<ResultPayroll> resultPayrolls);
}
