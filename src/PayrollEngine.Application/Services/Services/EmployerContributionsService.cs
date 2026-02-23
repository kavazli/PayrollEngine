
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Services;
using PayrollEngine.Domain.Interfaces.Providers;


namespace PayrollEngine.Application.Services.Services;


// Bu sınıf, işveren katkıları ile ilgili işlemleri gerçekleştiren servis sınıfıdır.
// Bu sınıf, IEmployerContributionsProvider arayüzünü kullanarak işveren katkılarını
// eklemek, almak, temizlemek ve güncellemek için yöntemler sağlar.
public class EmployerContributionsService : IEmployerContributionsService
{   

    private readonly IEmployerContributionsProvider _employerContributionsProvider;

    public EmployerContributionsService(IEmployerContributionsProvider employerContributionsProvider)
    {   

        if (employerContributionsProvider == null)
        {
            throw new ArgumentNullException(nameof(employerContributionsProvider), "Employer contributions provider cannot be null.");
        }

        _employerContributionsProvider = employerContributionsProvider;
    }


    // İşveren katkılarını asenkron olarak ekler. 
    // Bu yöntem, geçerli bir katkı sağlanmazsa bir ArgumentNullException fırlatır.
    public async Task<EmployerContributions> AddAsync(EmployerContributions employerContributions)
    {   
        if (employerContributions == null)
        {
            throw new ArgumentNullException(nameof(employerContributions), "Employer contributions cannot be null.");
        }

        await _employerContributionsProvider.AddAsync(employerContributions);
        return employerContributions;
        
    }


    // Birden fazla işveren katkısını asenkron olarak ekler.
    // Bu yöntem, geçerli bir katkı listesi sağlanmazsa bir ArgumentNullException fırlatır.
    public async Task<List<EmployerContributions>> AddRangeAsync(List<EmployerContributions> employerContributionsList)
    {
       if (employerContributionsList == null)
        {
            throw new ArgumentNullException(nameof(employerContributionsList), "Employer contributions list cannot be null.");
        }

        await _employerContributionsProvider.AddRangeAsync(employerContributionsList);
        return employerContributionsList;
    }


    // İşveren katkılarını asenkron olarak temizler.
    public Task ClearAsync()
    {
        return _employerContributionsProvider.ClearAsync();
    }

    // İşveren katkılarını asenkron olarak alır.
    public Task<List<EmployerContributions>> GetAsync()
    {
        return _employerContributionsProvider.GetAsync();
    }

    

}
