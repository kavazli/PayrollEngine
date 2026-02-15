using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces;

public interface ISSCeilingProvider
{
    Task<SSCeiling> GetValueAsync(int year);
}
