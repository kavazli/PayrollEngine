using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces;

public interface IIncomeTaxBracketsProvider
{
    Task<List<IncomeTaxBracket>> GetValueAsync(int year);
}
