using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Domain.Interfaces.Params;

public interface IDisabilityDegreeProvider
{
    public Task<List<DisabilityDegree>> GetValueAsync(int year);
    
}
