

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;


namespace PayrollEngine.Application.Services.Params;


// Gelir vergisi dilimlerini sağlayan servis sınıfı.
// Bu sınıf, gelir vergisi dilimlerini almak için bir yöntem içerir.
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


    // Gelir vergisi dilimlerini almak için kullanılan yöntem.
    // Bu yöntem, geçersiz bir yıl değeri sağlanması durumunda bir ArgumentOutOfRangeException fırlatır.
    public async Task<List<IncomeTaxBracket>> GetValueAsync(int year)
    {   
        if(year < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(year), "Year cannot be negative.");
        }
        var result = await _incomeTaxBracketsProvider.GetValueAsync(year);    

        return result;
    }

}
