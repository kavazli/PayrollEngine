


using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces.Providers;
using PayrollEngine.Domain.Interfaces.Services;


namespace PayrollEngine.Application.Services.Services;


// Bu sınıf, maaş ayları ile ilgili işlemleri gerçekleştiren servis sınıfıdır.
// Bu sınıf, IPayrollMonthsProvider arayüzünü kullanarak maaş aylarını 
// eklemek, almak, temizlemek ve güncellemek için yöntemler sağlar. 
public class PayrollMonthService : IPayrollMonthService
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


    // Belirli bir çalışan senaryosu ve şablon ayları listesi için maaş aylarını asenkron olarak ekler.
    // Bu yöntem, geçerli bir şablon ayları listesi veya çalışan senaryosu sağlanmazsa bir ArgumentException veya ArgumentNullException fırlatır.  
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


    // Maaş aylarını asenkron olarak alır.
    public Task<List<PayrollMonth>> GetAsync()
    {
        return _payrollMonthsProvider.GetAsync();
    }


    public Task<PayrollMonth> GetMonthAsync(Months month)
    {
        return _payrollMonthsProvider.GetMonthAsync(month);
    }


    // Maaş aylarını asenkron olarak temizler.
    public async Task ClearAsync()
    {
        await _payrollMonthsProvider.ClearAsync();
    }


    // Belirli bir çalışan senaryosu ve şablon ayları listesi için maaş aylarını asenkron olarak günceller.
    // Bu yöntem, geçerli bir şablon ayları listesi veya çalışan senaryosu sağlanmazsa bir ArgumentException veya ArgumentNullException fırlatır.
    public async Task SetAsync(List<PayrollMonth> months)
    {
        if (months == null || months.Count == 0)
        {
            throw new ArgumentException("Months list cannot be null or empty.", nameof(months));
        }
        await _payrollMonthsProvider.SetAsync(months);
    }
}
