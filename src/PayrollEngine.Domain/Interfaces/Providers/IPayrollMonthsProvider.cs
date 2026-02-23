

using PayrollEngine.Domain.Entities;



namespace PayrollEngine.Domain.Interfaces.Providers;


// Maaş aylarını sağlayan provider arayüzü.
// Bu arayüz, maaş aylarını eklemek, almak, temizlemek ve 
// belirli bir maaş ayını güncellemek için yöntemler içerir.
public interface IPayrollMonthsProvider
{
    Task<List<PayrollMonth>> AddAsync(List<PayrollMonth> months);
    Task<List<PayrollMonth>> GetAsync();
    Task ClearAsync();
    Task SetAsync(List<PayrollMonth> months);
}
