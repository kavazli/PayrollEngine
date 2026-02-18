using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Application.Services.Params;

public class StampTaxService
{

    private readonly IStampTaxProvider _stampTaxProvider;


    public StampTaxService(IStampTaxProvider stampTaxProvider)
    {   
        if (stampTaxProvider == null)
        {
            throw new ArgumentNullException(nameof(stampTaxProvider));
        }
        _stampTaxProvider = stampTaxProvider;
    }


    public async Task<StampTax> GetValueAsync(int year)
    {
        return await _stampTaxProvider.GetValueAsync(year);
    }
}
