using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces;


// SGK tavan bilgilerini sağlayan provider arayüzü.
// Bu arayüz, belirli bir yıl için SGK tavan bilgilerini almak için
public interface ISSCeilingProvider
{
    Task<SSCeiling> GetValueAsync(int year);
}
