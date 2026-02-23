

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces;


namespace PayrollEngine.Application.Services.Params;


// Asgari ücret bilgisini sağlayan servis sınıfı.
// Bu sınıf, asgari ücret bilgisini almak için bir yöntem içerir.
public class MinimumWageService
{
    private readonly IMinimumWageProvider _minimumWageProvider;


    public MinimumWageService(IMinimumWageProvider minimumWageProvider)
    {
        if (minimumWageProvider == null)
        {
            throw new ArgumentNullException(nameof(minimumWageProvider));
        }
        _minimumWageProvider = minimumWageProvider;
    }


    // Asgari ücret bilgisini almak için kullanılan yöntem.
    public async Task<MinimumWage> GetValueAsync(int year)
    {
        return await _minimumWageProvider.GetValueAsync(year);
    }
}
