using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Templates;

namespace PayrollEngine.Application.Services;

public class EmployeeScenariosService
{

    private readonly IEmployeeScenariosProvider _employeeScenariosProvider;


    public EmployeeScenariosService(IEmployeeScenariosProvider provider)
    {
        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider), "Employee scenarios provider cannot be null.");
        }

        _employeeScenariosProvider = provider;

    }


    public async Task<EmployeeScenario> AddAsync(EmployeeScenario scenario)
    {

        if (_employeeScenariosProvider == null)
        {
            throw new InvalidOperationException("Employee scenarios provider is not initialized.");
        }
        if (scenario == null)
        {
            throw new ArgumentNullException(nameof(scenario), "Employee scenario cannot be null.");
        }   

        await ClearAsync();

        await _employeeScenariosProvider.AddAsync(scenario);
        return scenario;

    }

    public async Task<EmployeeScenario> GetAsync()
    {
        return await _employeeScenariosProvider.GetAsync();
    }


    public async Task ClearAsync()
    {
        await _employeeScenariosProvider.ClearAsync();
    }


    public async Task SetAsync(EmployeeScenario scenario)
    {
        if (scenario == null)
        {
            throw new ArgumentNullException(nameof(scenario), "Employee scenario cannot be null.");
        }

        await _employeeScenariosProvider.SetAsync(scenario);
    }


}