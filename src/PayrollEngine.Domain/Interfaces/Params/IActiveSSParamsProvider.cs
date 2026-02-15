using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces;

public interface IActiveSSParamsProvider
{
    Task<ActiveSSParams> GetValueAsync(int year);  
}
