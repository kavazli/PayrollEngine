using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Application.Services.Params;

public class CumulativeIncomeTaxBaseService
{

    private readonly ICumulativeIncomeTaxBaseProvider _cumulativeIncomeTaxBaseProvider;


    public CumulativeIncomeTaxBaseService(ICumulativeIncomeTaxBaseProvider cumulativeIncomeTaxBaseProvider)
    {
        if (cumulativeIncomeTaxBaseProvider == null)
        {
            throw new ArgumentNullException(nameof(cumulativeIncomeTaxBaseProvider));
        }
        _cumulativeIncomeTaxBaseProvider = cumulativeIncomeTaxBaseProvider;
    }


    public Task<CumulativeIncomeTaxBase> GetValueAsync(Months month)
    {
        return _cumulativeIncomeTaxBaseProvider.GetValueAsync(month);
    }


    public Task<CumulativeIncomeTaxBase> AddAsync(CumulativeIncomeTaxBase taxBase)
    {
        return _cumulativeIncomeTaxBaseProvider.AddAsync(taxBase);
    }


    public Task ClearAsync()
    {
        return _cumulativeIncomeTaxBaseProvider.ClearAsync();
    }
    
}
