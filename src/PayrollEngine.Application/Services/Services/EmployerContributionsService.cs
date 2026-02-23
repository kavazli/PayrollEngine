using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Services;

namespace PayrollEngine.Application.Services.Services;

public class EmployerContributionsService : IEmployerContributionsService
{
    public Task<EmployerContributions> AddAsync(EmployerContributions employerContributions)
    {
        throw new NotImplementedException();
    }

    public Task<List<EmployerContributions>> AddRangeAsync(List<EmployerContributions> employerContributionsList)
    {
        throw new NotImplementedException();
    }

    public Task ClearAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<EmployerContributions>> GetAsync()
    {
        throw new NotImplementedException();
    }
}
