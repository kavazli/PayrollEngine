using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Providers;

public interface IPrivateHealthInsuranceProvider
{
    Task<PrivateHealthInsurance> AddAsync(PrivateHealthInsurance healthInsurance);
    Task<PrivateHealthInsurance> GetAsync();
    Task ClearAsync();
    Task SetAsync(PrivateHealthInsurance healthInsurance);
}
