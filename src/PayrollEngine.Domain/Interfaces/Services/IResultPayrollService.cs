

using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces.Services;


// Bordro sonuçlarını yöneten servis arayüzü.
// Bu arayüz, bordro sonuçlarını eklemek, almak, temizlemek ve
// belirli bir bordro sonucunu güncellemek için yöntemler içerir.
public interface IResultPayrollService
{
    Task<ResultPayroll> AddAsync(ResultPayroll resultPayroll);
    Task<List<ResultPayroll>> AddRangeAsync(List<ResultPayroll> resultPayrolls);
    Task ClearAsync();
    Task<List<ResultPayroll>> GetAsync();
}
