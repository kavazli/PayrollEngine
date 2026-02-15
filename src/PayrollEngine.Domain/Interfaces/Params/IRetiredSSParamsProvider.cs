using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces;

public interface IRetiredSSParamsProvider
{
    Task<RetiredSSParams> GetValueAsync(int year);
}
