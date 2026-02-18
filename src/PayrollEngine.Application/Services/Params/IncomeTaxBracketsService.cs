using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Application.Services.Params;

public class IncomeTaxBracketsService
{

    private readonly IIncomeTaxBracketsProvider _incomeTaxBracketsProvider;


    public IncomeTaxBracketsService(IIncomeTaxBracketsProvider incomeTaxBracketsProvider)
    {
        if (incomeTaxBracketsProvider == null)
        {
            throw new ArgumentNullException(nameof(incomeTaxBracketsProvider));
        }
        _incomeTaxBracketsProvider = incomeTaxBracketsProvider;
    }


    public async Task<List<IncomeTaxBracket>> GetValueAsync(int year)
    {   
        
        var result = await _incomeTaxBracketsProvider.GetValueAsync(year);    

        return result;
    }

}
