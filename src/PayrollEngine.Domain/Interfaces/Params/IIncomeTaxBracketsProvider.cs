using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces;


// Gelir vergisi dilimlerini sağlayan provider arayüzü.
// Bu arayüz, belirli bir yıl için gelir vergisi dilimlerini almak için bir yöntem içerir.
public interface IIncomeTaxBracketsProvider
{
    Task<List<IncomeTaxBracket>> GetValueAsync(int year);
}
