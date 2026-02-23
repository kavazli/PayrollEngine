using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Services;

public interface IPrivateHealthInsuranceService
{
    Task<PrivateHealthInsurance> AddAsync(PrivateHealthInsurance privateHealthInsurance);
    Task<List<PrivateHealthInsurance>> AddRangeAsync(List<PrivateHealthInsurance> privateHealthInsuranceList);
    Task ClearAsync();
    Task<List<PrivateHealthInsurance>> GetAsync();
}
