using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces;

public interface IMinimumWageProvider
{
    Task<MinimumWage> GetValueAsync(int year);
}
