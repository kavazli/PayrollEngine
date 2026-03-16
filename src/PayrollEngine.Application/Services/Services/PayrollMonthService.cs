


using PayrollEngine.Application.Calculators;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces.Providers;
using PayrollEngine.Domain.Interfaces.Services;


namespace PayrollEngine.Application.Services.Services;


// Bu sınıf, maaş ayları ile ilgili işlemleri gerçekleştiren servis sınıfıdır.
// Bu sınıf, IPayrollMonthsProvider arayüzünü kullanarak maaş aylarını 
// eklemek, almak, temizlemek ve güncellemek için yöntemler sağlar. 
public class PayrollMonthService : IPayrollMonthService
{   

    private readonly IPayrollMonthsProvider _payrollMonthsProvider;
    private readonly IEmployeeScenariosProvider _employeeScenariosProvider;
    private readonly IResultPayrollService _resultPayrollService;
    private readonly ResultPayrollCalc _resultPayrollCalc;
    private readonly NetSalaryIteration _netSalaryIteration;
    private readonly IShoppingVoucherService _shoppingVoucherService;
    private readonly ShoppingVoucherCalc _shoppingVoucherCalc;
    private readonly EmployerContributionsCalc _employerContributionsCalc;
    private readonly IEmployerContributionsService _employerContributionsService;


    public PayrollMonthService(IPayrollMonthsProvider provider,
                               IEmployeeScenariosProvider employeeScenariosProvider,
                               IResultPayrollService resultPayrollService,
                               ResultPayrollCalc resultPayrollCalc,
                               NetSalaryIteration netSalaryIteration,
                               IShoppingVoucherService shoppingVoucherService,
                               ShoppingVoucherCalc shoppingVoucherCalc,
                               EmployerContributionsCalc employerContributionsCalc,
                               IEmployerContributionsService employerContributionsService) 
    {   

        if (provider == null)
        {
            throw new ArgumentNullException(nameof(provider), "Payroll months provider cannot be null.");
        }

        if (employeeScenariosProvider == null)
        {
            throw new ArgumentNullException(nameof(employeeScenariosProvider), "Employee scenarios provider cannot be null.");
        }

        if (resultPayrollCalc == null)
        {
            throw new ArgumentNullException(nameof(resultPayrollCalc), "Result payroll calculator cannot be null.");
        }

        if (resultPayrollService == null)
        {
            throw new ArgumentNullException(nameof(resultPayrollService), "Result payroll service cannot be null.");
        }

        if (netSalaryIteration == null)
        {
            throw new ArgumentNullException(nameof(netSalaryIteration), "Net salary iteration cannot be null.");
        }

        if(shoppingVoucherService == null)
        {
            throw new ArgumentNullException(nameof(shoppingVoucherService), "Shopping voucher service cannot be null.");
        }

        if(shoppingVoucherCalc == null)
        {
            throw new ArgumentNullException(nameof(shoppingVoucherCalc), "Shopping voucher calculator cannot be null.");
        }

        if(employerContributionsCalc == null)
        {
            throw new ArgumentNullException(nameof(employerContributionsCalc), "Employer contributions calculator cannot be null.");
        }

        if(employerContributionsService == null)
        {
            throw new ArgumentNullException(nameof(employerContributionsService), "Employer contributions service cannot be null.");
        }

        _payrollMonthsProvider = provider;
        _employeeScenariosProvider = employeeScenariosProvider;
        _resultPayrollService = resultPayrollService; 
        _resultPayrollCalc = resultPayrollCalc;
        _netSalaryIteration = netSalaryIteration;
        _shoppingVoucherService = shoppingVoucherService;
        _shoppingVoucherCalc = shoppingVoucherCalc;
        _employerContributionsCalc = employerContributionsCalc;
        _employerContributionsService = employerContributionsService;
    }


    // Belirli bir çalışan senaryosu ve şablon ayları listesi için maaş aylarını asenkron olarak ekler.
    // Bu yöntem, geçerli bir şablon ayları listesi veya çalışan senaryosu sağlanmazsa bir ArgumentException veya ArgumentNullException fırlatır.  
    public async Task<List<PayrollMonth>> AddAsync(
        List<PayrollTemplateMonth> templateMonths, 
        EmployeeScenario scenario)
    {   

        if(templateMonths == null || templateMonths.Count == 0)
        {
            throw new ArgumentException("Template months list cannot be null or empty.", nameof(templateMonths));
        }
        if(scenario == null)
        {
            throw new ArgumentNullException(nameof(scenario), "Employee scenario cannot be null.");
        }
        
        // DB yi temizle
        await ClearAsync();

        var normalizedMonths = new List<PayrollMonth>();

        foreach (var templateMonth in templateMonths)
        {
            // Normalizasyon işlemi
            var normalizer = new PayrollMonthNormalizer(templateMonth, scenario);
            var normalizedMonth = normalizer.Normalize();
            normalizedMonths.Add(normalizedMonth);
        }
        
        await _payrollMonthsProvider.AddAsync(normalizedMonths);

        await CalculateResultPayrollAsync();




        return normalizedMonths;

    }


    // Maaş aylarını asenkron olarak alır.
    public Task<List<PayrollMonth>> GetAsync()
    {
        return _payrollMonthsProvider.GetAsync();
    }


    public Task<PayrollMonth> GetMonthAsync(Months month)
    {
        return _payrollMonthsProvider.GetMonthAsync(month);
    }


    // Maaş aylarını asenkron olarak temizler.
    public async Task ClearAsync()
    {
        await _payrollMonthsProvider.ClearAsync();
    }


    // Belirli bir çalışan senaryosu ve şablon ayları listesi için maaş aylarını asenkron olarak günceller.
    // Bu yöntem, geçerli bir şablon ayları listesi veya çalışan senaryosu sağlanmazsa bir ArgumentException veya ArgumentNullException fırlatır.
    public async Task SetAsync(List<PayrollMonth> months)
    {
        if (months == null || months.Count == 0)
        {
            throw new ArgumentException("Months list cannot be null or empty.", nameof(months));
        }
        await _payrollMonthsProvider.SetAsync(months);
    }


    private async Task CalculateResultPayrollAsync()
    {
        var employeeScenario = await _employeeScenariosProvider.GetAsync();

        var monthsPayroll = await _payrollMonthsProvider.GetAsync();

        var list = monthsPayroll.OrderBy(x => x.Month).ToList();

        List<ResultPayroll> resultPayrolls = new List<ResultPayroll>();
        List<ShoppingVoucher> shoppingVouchers = new List<ShoppingVoucher>();
        List<EmployerContributions> employerContributionsList = new List<EmployerContributions>();


       if(employeeScenario.SalaryInputType == SalaryInputType.Net)
       {

            foreach(var item in list)
            {
                var pay = await _netSalaryIteration.Iterator(item);
                resultPayrolls.Add(pay);

                
            }

            await _resultPayrollService.ClearAsync();
            await _resultPayrollService.AddRangeAsync(resultPayrolls);

            foreach(var item in list)
            {
                var shoppingVoucher = await _shoppingVoucherCalc.Calc(item, item.Month);
                shoppingVouchers.Add(shoppingVoucher);
            }

            await _shoppingVoucherService.ClearAsync();
            await _shoppingVoucherService.AddRangeAsync(shoppingVouchers);

            foreach( var item in list)
            {
                var employerContributions = await _employerContributionsCalc.Calc(item.Month);
                employerContributionsList.Add(employerContributions);                
            }

            await _employerContributionsService.ClearAsync();
            await _employerContributionsService.AddRangeAsync(employerContributionsList);

 
        }
        else
        {
            foreach(var item in list)
            {
                var pay = await _resultPayrollCalc.Calc(item);
                resultPayrolls.Add(pay);

                
            }

            await _resultPayrollService.ClearAsync();
            await _resultPayrollService.AddRangeAsync(resultPayrolls);

            foreach(var item in list)
            {
                var shoppingVoucher = await _shoppingVoucherCalc.Calc(item, item.Month);
                shoppingVouchers.Add(shoppingVoucher);
            }

            await _shoppingVoucherService.ClearAsync();
            await _shoppingVoucherService.AddRangeAsync(shoppingVouchers);


            foreach( var item in list)
            {
                var employerContributions = await _employerContributionsCalc.Calc(item.Month);
                employerContributionsList.Add(employerContributions);                
            }

            await _employerContributionsService.ClearAsync();
            await _employerContributionsService.AddRangeAsync(employerContributionsList);




        }
    }


}
