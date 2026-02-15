using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces;

public interface IStampTaxProvider
{
    Task<StampTax> GetValueAsync(int year);
}
