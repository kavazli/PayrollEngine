using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Domain.Interfaces;

public interface ICumulativeIncomeTaxBaseProvider
{
    Task<CumulativeIncomeTaxBase> GetValueAsync(Months month);
    Task<CumulativeIncomeTaxBase> AddAsync(CumulativeIncomeTaxBase taxBase);
    Task ClearAsync();
}
