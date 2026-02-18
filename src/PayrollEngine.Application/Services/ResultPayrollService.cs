using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Templates;

namespace PayrollEngine.Application.Services;

public class ResultPayrollService
{   

    private readonly IResultPayrollsProvider _resultPayrollsProvider;
    

    public ResultPayrollService(IResultPayrollsProvider provider)
    {   

        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider), "Result payrolls provider cannot be null.");
        }
        _resultPayrollsProvider = provider;
        
    }


    public async Task<ResultPayroll> AddAsync(ResultPayroll resultPayroll)
    {   

        if (resultPayroll == null)
        {
            throw new ArgumentNullException(nameof(resultPayroll), "Result payroll cannot be null.");
        }
        await _resultPayrollsProvider.AddAsync(resultPayroll);
        return resultPayroll;
       
    }


    public async Task<List<ResultPayroll>> AddRangeAsync(List<ResultPayroll> resultPayrolls)
    {   
        if (resultPayrolls == null || !resultPayrolls.Any())
        {
            throw new ArgumentException("Result payrolls list cannot be null or empty.", nameof(resultPayrolls));
        }
        await _resultPayrollsProvider.AddRangeAsync(resultPayrolls);
        return resultPayrolls;
       
    }


    public async Task<List<ResultPayroll>> GetAsync()
    {   
        return await _resultPayrollsProvider.GetAsync();
       
    }


    public async Task ClearAsync()
    {   
        await _resultPayrollsProvider.ClearAsync();
       
    }
}
