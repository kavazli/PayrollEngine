using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Providers;

public interface IPrivateHealthInsuranceProvider
{
    Task<PrivateHealthInsurance> AddAsync(PrivateHealthInsurance healthInsurance);
    Task<List<PrivateHealthInsurance>> AddRangeAsync(List<PrivateHealthInsurance> healthInsuranceList);
    Task<List<PrivateHealthInsurance>> GetAsync();
    Task ClearAsync();
    Task SetAsync(PrivateHealthInsurance healthInsurance);
}
