using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Application.Services.Params;

public class SSCeilingService
{

    private readonly ISSCeilingProvider _ssCeilingProvider;


    public SSCeilingService(ISSCeilingProvider ssCeilingProvider)
    {
        if (ssCeilingProvider == null)
        {
            throw new ArgumentNullException(nameof(ssCeilingProvider));
        }
        _ssCeilingProvider = ssCeilingProvider;
    }


    public async Task<SSCeiling> GetValueAsync(int year)
    {
        return await _ssCeilingProvider.GetValueAsync(year);
    }

}

