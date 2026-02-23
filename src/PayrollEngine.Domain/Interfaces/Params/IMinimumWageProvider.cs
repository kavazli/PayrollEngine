using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces;


// Asgari ücret bilgilerini sağlayan provider arayüzü.
// Bu arayüz, belirli bir yıl için asgari ücret bilgilerini almak için bir yöntem içerir.
public interface IMinimumWageProvider
{
    Task<MinimumWage> GetValueAsync(int year);
}
