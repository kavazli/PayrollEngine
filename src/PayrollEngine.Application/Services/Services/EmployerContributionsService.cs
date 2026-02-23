using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Services;
using PayrollEngine.Domain.Interfaces.Providers;

namespace PayrollEngine.Application.Services.Services;

public class EmployerContributionsService : IEmployerContributionsService
{   

    private readonly IEmployerContributionsProvider _employerContributionsProvider;

    public EmployerContributionsService(IEmployerContributionsProvider employerContributionsProvider)
    {   

        if (employerContributionsProvider == null)
        {
            throw new ArgumentNullException(nameof(employerContributionsProvider), "Employer contributions provider cannot be null.");
        }

        _employerContributionsProvider = employerContributionsProvider;
    }



    public async Task<EmployerContributions> AddAsync(EmployerContributions employerContributions)
    {   
        if (employerContributions == null)
        {
            throw new ArgumentNullException(nameof(employerContributions), "Employer contributions cannot be null.");
        }

        await _employerContributionsProvider.AddAsync(employerContributions);
        return employerContributions;
        
    }


    public async Task<List<EmployerContributions>> AddRangeAsync(List<EmployerContributions> employerContributionsList)
    {
       if (employerContributionsList == null)
        {
            throw new ArgumentNullException(nameof(employerContributionsList), "Employer contributions list cannot be null.");
        }

        await _employerContributionsProvider.AddRangeAsync(employerContributionsList);
        return employerContributionsList;
    }


    public Task ClearAsync()
    {
        return _employerContributionsProvider.ClearAsync();
    }


    public Task<List<EmployerContributions>> GetAsync()
    {
        return _employerContributionsProvider.GetAsync();
    }

    

}
