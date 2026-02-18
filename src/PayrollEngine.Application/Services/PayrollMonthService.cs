using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Templates;

namespace PayrollEngine.Application.Services;


public class PayrollMonthService
{   

    private readonly IPayrollMonthsProvider _payrollMonthsProvider;


    public PayrollMonthService(IPayrollMonthsProvider provider)
    {   

        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider), "Payroll months provider cannot be null.");
        }
        _payrollMonthsProvider = provider;

    }

    
    public async Task<List<PayrollMonth>> AddAsync(
        List<PayrollTemplateMonth> templateMonths, 
        EmployeeScenario scenario)
    {   

        if(templateMonths == null || templateMonths.Count == 0)
        {
            throw new ArgumentException("Template months list cannot be null or empty.", nameof(templateMonths));
        }
        if(scenario == null)
        {
            throw new ArgumentNullException(nameof(scenario), "Employee scenario cannot be null.");
        }
        
        // DB yi temizle
        await ClearAsync();

        var normalizedMonths = new List<PayrollMonth>();

        foreach (var templateMonth in templateMonths)
        {
            // Normalizasyon işlemi
            var normalizer = new PayrollMonthNormalizer(templateMonth, scenario);
            var normalizedMonth = normalizer.Normalize();
            normalizedMonths.Add(normalizedMonth);
        }
        
        await _payrollMonthsProvider.AddAsync(normalizedMonths);
        return normalizedMonths;

    }


    public Task<List<PayrollMonth>> GetAsync()
    {
        return _payrollMonthsProvider.GetAsync();
    }


    public async Task ClearAsync()
    {
        await _payrollMonthsProvider.ClearAsync();
    }


    public async Task SetAsync(List<PayrollMonth> months)
    {
        if (months == null || months.Count == 0)
        {
            throw new ArgumentException("Months list cannot be null or empty.", nameof(months));
        }
        await _payrollMonthsProvider.SetAsync(months);
    }
}
