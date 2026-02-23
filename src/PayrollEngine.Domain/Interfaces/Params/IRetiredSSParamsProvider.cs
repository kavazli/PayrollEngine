using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces;


// Emekli SGK parametrelerini sağlayan provider arayüzü.
// Bu arayüz, belirli bir yıl için emekli SGK parametrelerini
public interface IRetiredSSParamsProvider
{
    Task<RetiredSSParams> GetValueAsync(int year);
}
