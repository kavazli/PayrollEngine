using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Services;

namespace PayrollEngine.Application.Services.Services;

public class PrivateHealthInsuranceService : IPrivateHealthInsuranceService
{
    public Task<PrivateHealthInsurance> AddAsync(PrivateHealthInsurance privateHealthInsurance)
    {
        throw new NotImplementedException();
    }

    public Task<List<PrivateHealthInsurance>> AddRangeAsync(List<PrivateHealthInsurance> privateHealthInsuranceList)
    {
        throw new NotImplementedException();
    }

    public Task ClearAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<PrivateHealthInsurance>> GetAsync()
    {
        throw new NotImplementedException();
    }
}
