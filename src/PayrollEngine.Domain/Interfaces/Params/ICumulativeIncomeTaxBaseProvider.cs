using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces;

public interface ICumulativeIncomeTaxBaseProvider
{
    Task<CumulativeIncomeTaxBase> GetValueAsync(int month);
}
