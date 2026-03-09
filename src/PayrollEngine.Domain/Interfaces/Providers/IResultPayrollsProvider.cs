

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;


namespace PayrollEngine.Domain.Interfaces.Providers;


// Sonuç maaşlarını sağlayan provider arayüzü.
// Bu arayüz, sonuç maaşlarını eklemek, almak, temizlemek ve
// belirli bir sonuç maaşını güncellemek için yöntemler içerir.
public interface IResultPayrollsProvider
{   
    Task<ResultPayroll> AddAsync(ResultPayroll resultPayroll);
    Task<List<ResultPayroll>> AddRangeAsync(List<ResultPayroll> resultPayrolls);
    Task<List<ResultPayroll>> GetAsync();
    Task<ResultPayroll> GetMonthAsync(Months month);
    Task ClearAsync();
    Task SetAsync(List<ResultPayroll> resultPayrolls);
}
