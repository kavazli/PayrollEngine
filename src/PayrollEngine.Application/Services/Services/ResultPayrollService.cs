

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces.Providers;
using PayrollEngine.Domain.Interfaces.Services;


namespace PayrollEngine.Application.Services.Services;


// Bu sınıf, sonuç bordrosu ile ilgili işlemleri gerçekleştiren servis sınıfıdır.
// Bu sınıf, IResultPayrollsProvider arayüzünü kullanarak sonuç bordrosunu
// eklemek, almak, temizlemek ve güncellemek için yöntemler sağlar.
public class ResultPayrollService : IResultPayrollService
{   

    private readonly IResultPayrollsProvider _resultPayrollsProvider;
    

    public ResultPayrollService(IResultPayrollsProvider provider)
    {   

        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider), "Result payrolls provider cannot be null.");
        }
        _resultPayrollsProvider = provider;
        
    }


    // Sonuç bordrosunu asenkron olarak ekler. 
    // Bu yöntem, geçerli bir sonuç bordrosu sağlanmazsa bir ArgumentNullException fırlatır.
    public async Task<ResultPayroll> AddAsync(ResultPayroll resultPayroll)
    {   

        if (resultPayroll == null)
        {
            throw new ArgumentNullException(nameof(resultPayroll), "Result payroll cannot be null.");
        }
        await _resultPayrollsProvider.AddAsync(resultPayroll);
        return resultPayroll;
       
    }

    // Birden fazla sonuç bordrosunu asenkron olarak ekler.
    // Bu yöntem, geçerli bir sonuç bordrosu listesi sağlanmazsa bir ArgumentNullException fırlatır.
    public async Task<List<ResultPayroll>> AddRangeAsync(List<ResultPayroll> resultPayrolls)
    {   
        if (resultPayrolls == null || !resultPayrolls.Any())
        {
            throw new ArgumentException("Result payrolls list cannot be null or empty.", nameof(resultPayrolls));
        }
        await _resultPayrollsProvider.AddRangeAsync(resultPayrolls);
        return resultPayrolls;
       
    }


    // Sonuç bordrosunu asenkron olarak alır.
    public async Task<List<ResultPayroll>> GetAsync()
    {   
        return await _resultPayrollsProvider.GetAsync();
       
    }


    // Belirli bir aya ait sonuç bordrosunu asenkron olarak alır.
    public async Task<ResultPayroll> GetMonthAsync(Months month)
    {
        return await _resultPayrollsProvider.GetMonthAsync(month);
    }


    // Sonuç bordrosunu asenkron olarak temizler.
    public async Task ClearAsync()
    {   
        await _resultPayrollsProvider.ClearAsync();
       
    }
}
