using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PayrollEngine.Application.Services;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Infrastructure;
using PayrollEngine.Infrastructure.Providers.Templates;


class Program
{
    static async Task Main(string[] args)
    {   



        
        var options = new DbContextOptionsBuilder<PayrollEngineDbContext>()
            .UseSqlite(
                "Data Source=../../src/PayrollEngine.Infrastructure/PayrollEngine.db")
            .Options;

        using var context = new PayrollEngineDbContext(options);

        PayrollMonthsProvider payrollMonthsProvider = new PayrollMonthsProvider(context);
        EmployeeScenariosService employeeScenariosService = new EmployeeScenariosService(new EmployeeScenariosProvider(context))    ;
        PayrollMonthService payrollMonthService = new PayrollMonthService(payrollMonthsProvider);
        
       

        List<PayrollTemplateMonth> payrollMonths = new List<PayrollTemplateMonth>();

        for(int i = 1; i <= 12; i++)
        {
            var payrollMonth = new PayrollTemplateMonth
            {   
                Month = Months.May,
                WorkDay = 30,
                BaseSalary = 25000m,
                SalaryIncreaseRate = 0.25m,
                Overtime50 = 25m,
                Overtime100 = 25m,
                BonusAmount = 1000m,
                PrivateHealthInsurance = 200m,
                ShoppingVoucher = 100m
            };

            payrollMonths.Add(payrollMonth);
            
        }


        await payrollMonthService.ProcessAndSaveBatchAsync(payrollMonths, await employeeScenariosService.GetEmployeeScenarioAsync());


    }

}