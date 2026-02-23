using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Services;

public interface IResultPayrollService
{
    Task<ResultPayroll> AddAsync(ResultPayroll resultPayroll);
    Task<List<ResultPayroll>> AddRangeAsync(List<ResultPayroll> resultPayrolls);
    Task ClearAsync();
    Task<List<ResultPayroll>> GetAsync();
    

}
