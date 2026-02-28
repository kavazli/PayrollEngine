using System;
using System.Runtime.CompilerServices;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Params;

namespace PayrollEngine.Application.Services.Params;

public class DisabilityDegreeService
{
    private readonly IDisabilityDegreeProvider _disabilityDegreeProvider;

    public DisabilityDegreeService(IDisabilityDegreeProvider disabilityDegreeProvider)
    {
        if (disabilityDegreeProvider == null)
        {
            throw new ArgumentNullException(nameof(disabilityDegreeProvider));
        }

        _disabilityDegreeProvider = disabilityDegreeProvider;
    }

    public async Task<List<DisabilityDegree>> GetValueAsync(int year)
    {
        if(year < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(year), "Year cannot be negative.");
        }
        var result = await _disabilityDegreeProvider.GetValueAsync(year);    

        return result;
    }

}
