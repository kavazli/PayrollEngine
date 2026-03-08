

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Providers;
using PayrollEngine.Domain.Interfaces.Services;


namespace PayrollEngine.Application.Services.Services;


// Bu sınıf, çalışan senaryoları ile ilgili işlemleri gerçekleştiren servis sınıfıdır.
// Bu sınıf, IEmployeeScenariosProvider arayüzünü kullanarak çalışan senaryolarını
// eklemek, almak, temizlemek ve güncellemek için yöntemler sağlar.
public class EmployeeScenariosService : IEmployeeScenariosService
{

    private readonly IEmployeeScenariosProvider _employeeScenariosProvider;


    public EmployeeScenariosService(IEmployeeScenariosProvider provider)
    {
        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider), "Employee scenarios provider cannot be null.");
        }

        _employeeScenariosProvider = provider;

    }

    // Çalışan senaryosunu asenkron olarak ekler. 
    // Bu yöntem, geçerli bir senaryo sağlanmazsa bir ArgumentNullException fırlatır.
    public async Task<EmployeeScenario> AddAsync(EmployeeScenario scenario)
    {
       
        if (scenario == null)
        {
            throw new ArgumentNullException(nameof(scenario), "Employee scenario cannot be null.");
        }   

        scenario.Id = Guid.NewGuid();

        await ClearAsync();

        await _employeeScenariosProvider.AddAsync(scenario);
        return scenario;

    }


    // Çalışan senaryosunu asenkron olarak alır.
    public async Task<EmployeeScenario> GetAsync()
    {
        return await _employeeScenariosProvider.GetAsync();
    }


    // Çalışan senaryolarını asenkron olarak temizler.
    public async Task ClearAsync()
    {
        await _employeeScenariosProvider.ClearAsync();
    }


    // Belirli bir çalışan senaryosunu asenkron olarak günceller.
    // Bu yöntem, geçerli bir senaryo sağlanmazsa bir ArgumentNullException fırlatır.
    public async Task SetAsync(EmployeeScenario scenario)
    {
        if (scenario == null)
        {
            throw new ArgumentNullException(nameof(scenario), "Employee scenario cannot be null.");
        }

        await _employeeScenariosProvider.SetAsync(scenario);
    }


}