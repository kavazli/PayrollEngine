using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Application.Services.Params;

public class ActiveSSParamsService
{

    private readonly IActiveSSParamsProvider _activeSSParamsProvider;


    public ActiveSSParamsService(IActiveSSParamsProvider activeSSParamsProvider)
    {
        if (activeSSParamsProvider == null)
        {
            throw new ArgumentNullException(nameof(activeSSParamsProvider));
        }
        _activeSSParamsProvider = activeSSParamsProvider;   
    }


    public Task<ActiveSSParams> GetValueAsync(int year)
    {
        return _activeSSParamsProvider.GetValueAsync(year);
    }

}
