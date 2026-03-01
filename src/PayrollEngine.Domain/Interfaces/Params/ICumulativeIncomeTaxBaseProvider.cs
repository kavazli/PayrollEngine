using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;


namespace PayrollEngine.Domain.Interfaces;


// Gelir vergisi matrahının kümülatif olarak hesaplanması için gerekli verileri sağlayan provider arayüzü.
// Bu arayüz, belirli bir ay için kümülatif gelir vergisi matrahını almak, eklemek ve temizlemek için yöntemler içerir.
public interface ICumulativeIncomeTaxBaseProvider
{
    Task<CumulativeIncomeTaxBase?> GetValueAsync(Months month);
    Task<CumulativeIncomeTaxBase> AddAsync(CumulativeIncomeTaxBase taxBase);
    Task<CumulativeIncomeTaxBase> UpdateAsync(CumulativeIncomeTaxBase taxBase);
    Task ClearAsync();
}
