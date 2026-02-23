using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces;


// Damga vergisi bilgilerini sağlayan provider arayüzü.
// Bu arayüz, belirli bir yıl için damga vergisi bilgilerini almak için bir yöntem içerir.
public interface IStampTaxProvider
{
    Task<StampTax> GetValueAsync(int year);
}
