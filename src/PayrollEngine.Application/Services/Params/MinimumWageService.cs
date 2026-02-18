using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Application.Services.Params;

public class MinimumWageService
{
    private readonly IMinimumWageProvider _minimumWageProvider;


    public MinimumWageService(IMinimumWageProvider minimumWageProvider)
    {
        if (minimumWageProvider == null)
        {
            throw new ArgumentNullException(nameof(minimumWageProvider));
        }
        _minimumWageProvider = minimumWageProvider;
    }


    public async Task<MinimumWage> GetValueAsync(int year)
    {
        return await _minimumWageProvider.GetValueAsync(year);
    }
}
