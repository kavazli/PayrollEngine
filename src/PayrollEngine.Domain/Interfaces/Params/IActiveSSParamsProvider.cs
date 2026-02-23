using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces;


// Aktif SGK parametrelerini sağlayan provider arayüzü.
// Bu arayüz, belirli bir yıl için aktif SGK parametrelerini almak için bir yöntem içerir.
public interface IActiveSSParamsProvider
{
    Task<ActiveSSParams> GetValueAsync(int year);  
}
