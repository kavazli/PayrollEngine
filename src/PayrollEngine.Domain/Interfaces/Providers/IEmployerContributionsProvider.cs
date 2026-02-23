using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Providers;

public interface IEmployerContributionsProvider
{
    Task<EmployerContributions> AddAsync(EmployerContributions contributions);
    Task<List<EmployerContributions>> AddRangeAsync(List<EmployerContributions> contributionsList);
    Task<List<EmployerContributions>> GetAsync();
    Task ClearAsync();
    Task SetAsync(EmployerContributions contributions);  
}
