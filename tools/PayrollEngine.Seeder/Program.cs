using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PayrollEngine.Application;
using PayrollEngine.Application.Calculators;
using PayrollEngine.Application.Services;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Application.Services.Services;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces.Services;
using PayrollEngine.Infrastructure;

class Program
{
    static async Task Main(string[] args)
    {
        var services = new ServiceCollection();

        services.AddDbContext<PayrollEngineDbContext>(options =>
            options.UseSqlite("Data Source=../../src/PayrollEngine.Infrastructure/PayrollEngine.db"));

        services.AddInfrastructure();
        services.AddApplication();

        var serviceProvider = services.BuildServiceProvider();

        var ocak = serviceProvider.GetRequiredService<IPayrollMonthService>();
        var SGKTavanKontrol = serviceProvider.GetRequiredService<SSContributionCalc>();
        var SGKISciKesinti = serviceProvider.GetRequiredService<EmployeeSSCAmountCalc>();
        var SGKISciIszKesinti = serviceProvider.GetRequiredService<EmployeeUICAmountCalc>();
        var CumulativeIncomeTaxBase = serviceProvider.GetRequiredService<CumtIncomeTaxBaseCalc>();
        var IncomeTaxBaseCalc = serviceProvider.GetRequiredService<IncomeTaxBaseCalc>();
        var IncomeTax = serviceProvider.GetRequiredService<IncomeTaxCalc>();
        var IncomeTaxExemption = serviceProvider.GetRequiredService<IncomeTaxExemptionCalc>();
        var StampTax = serviceProvider.GetRequiredService<StampTaxCalc>();
        var StampTaxExemption = serviceProvider.GetRequiredService<StampTaxExemptionCalc>();
        var NetSalary = serviceProvider.GetRequiredService<NetSalaryCalc>();

        var result = serviceProvider.GetRequiredService<IResultPayrollService>();
        // var reslut2 = serviceProvider.GetRequiredService<CumulativeIncomeTaxBaseService>();

        // await result.ClearAsync();
        // await reslut2.ClearAsync();
            
        // var month = await ocak.GetAsync();
        // var ocakMonth = month.Where(x => x.Month == Months.January).FirstOrDefault();
        // if (ocakMonth == null)
        // {
        //     Console.WriteLine("Ocak ayi bulunamadi.");
        //     return;
        // }


       ResultPayroll resultPayroll= new ResultPayroll();

       resultPayroll.Month = Months.August;
       resultPayroll.WorkDay = 31;
       resultPayroll.CurrentSalary = 0m;
       resultPayroll.Overtime50Amount = 0m;
       resultPayroll.Overtime100Amount = 0m;
       resultPayroll.BonusAmount = 0m;
       resultPayroll.TotalSalary = 0m;

       resultPayroll.GrossSalary = 33030m;
       resultPayroll.SSContributionBase = await SGKTavanKontrol.Calc(resultPayroll.GrossSalary);
       resultPayroll.EmployeeSSContributionAmount = await SGKISciKesinti.Calc(resultPayroll.SSContributionBase);
       resultPayroll.EmployeeUIContributionAmount = await SGKISciIszKesinti.Calc(resultPayroll.SSContributionBase);
       resultPayroll.IncomeTaxBase = IncomeTaxBaseCalc.Calc(resultPayroll.GrossSalary, resultPayroll.EmployeeSSContributionAmount, resultPayroll.EmployeeUIContributionAmount);
       resultPayroll.CumulativeIncomeTaxBase = await CumulativeIncomeTaxBase.Calc(resultPayroll.Month, resultPayroll.IncomeTaxBase);
       resultPayroll.IncomeTax = await IncomeTax.Calc(resultPayroll.Month);
       resultPayroll.IncomeTaxExemption = await IncomeTaxExemption.Calc(resultPayroll.Month);
       resultPayroll.StampTax = await StampTax.Calc(resultPayroll.GrossSalary);
       resultPayroll.StampTaxExemption = await StampTaxExemption.Calc();
       resultPayroll.NetSalary = NetSalary.Calc(resultPayroll.GrossSalary, resultPayroll.EmployeeSSContributionAmount, resultPayroll.EmployeeUIContributionAmount, resultPayroll.IncomeTax, resultPayroll.StampTax);

        
        await result.AddAsync(resultPayroll);


        
    }
}