using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Services;
using PayrollEngine.Domain.Interfaces.Providers;

namespace PayrollEngine.Application.Services.Services;

public class PrivateHealthInsuranceService : IPrivateHealthInsuranceService
{   

    private readonly IPrivateHealthInsuranceProvider _healthInsuranceProvider;

    public PrivateHealthInsuranceService(IPrivateHealthInsuranceProvider healthInsuranceProvider)
    {   

        if (healthInsuranceProvider == null)
        {
            throw new ArgumentNullException(nameof(healthInsuranceProvider), "Health insurance provider cannot be null.");
        }

        _healthInsuranceProvider = healthInsuranceProvider;
    }


    public async Task<PrivateHealthInsurance> AddAsync(PrivateHealthInsurance privateHealthInsurance)
    {
        if (privateHealthInsurance == null)
        {
            throw new ArgumentNullException(nameof(privateHealthInsurance), "Private health insurance cannot be null.");
        }

        await _healthInsuranceProvider.AddAsync(privateHealthInsurance);
        return privateHealthInsurance;
    }


    public async Task<List<PrivateHealthInsurance>> AddRangeAsync(List<PrivateHealthInsurance> privateHealthInsuranceList)
    {
        if(privateHealthInsuranceList == null)
        {
            throw new ArgumentNullException(nameof(privateHealthInsuranceList), "Private health insurance list cannot be null.");
        }

        await _healthInsuranceProvider.AddRangeAsync(privateHealthInsuranceList);
        return privateHealthInsuranceList;
    }


    public Task ClearAsync()
    {
        return _healthInsuranceProvider.ClearAsync();
    }

    
    public Task<List<PrivateHealthInsurance>> GetAsync()
    {
        return _healthInsuranceProvider.GetAsync();
    }
}
