using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Services;

public interface IEmployerContributionsService
{
    Task<EmployerContributions> AddAsync(EmployerContributions employerContributions);
    Task<List<EmployerContributions>> AddRangeAsync(List<EmployerContributions> employerContributionsList);
    Task ClearAsync();
    Task<List<EmployerContributions>> GetAsync();
}
