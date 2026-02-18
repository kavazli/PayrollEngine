using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Application.Services.Params;

public class RetiredSSParamsService
{
    
    private readonly IRetiredSSParamsProvider _retiredSSParamsProvider;


    public RetiredSSParamsService(IRetiredSSParamsProvider retiredSSParamsProvider)
    {
        if (retiredSSParamsProvider == null)
        {
            throw new ArgumentNullException(nameof(retiredSSParamsProvider));
        }
        _retiredSSParamsProvider = retiredSSParamsProvider;
    }


    public async Task<RetiredSSParams> GetValueAsync(int year)
    {
        return await _retiredSSParamsProvider.GetValueAsync(year);
    }

}
