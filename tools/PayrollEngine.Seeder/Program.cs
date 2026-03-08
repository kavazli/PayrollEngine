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
using PayrollEngine.Infrastructure.Seed;

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

        var scenario = serviceProvider.GetRequiredService<IEmployeeScenariosService>();
        var paryollMonth = serviceProvider.GetRequiredService<IPayrollMonthService>();
        var resultPayroll = serviceProvider.GetRequiredService<IResultPayrollService>();
        var cumulativeIncomeTaxBaseService = serviceProvider.GetRequiredService<CumulativeIncomeTaxBaseService>();
        var resultPayrollCalc = serviceProvider.GetRequiredService<ResultPayrollCalc>();
        var netSalaryIteration = serviceProvider.GetRequiredService<NetSalaryIteration>();

        await resultPayroll.ClearAsync();
        await cumulativeIncomeTaxBaseService.ClearAsync();
        await scenario.ClearAsync();
        await paryollMonth.ClearAsync();
        
        

        EmployeeScenario employeeScenario = new EmployeeScenario
        {
           Year = 2026,
           SalaryInputType = SalaryInputType.Net,
           Status = Status.Active,
           DisabilityDegree = Degree.Normal,
           PayType = PayType.Monthly,
           IncentiveType = IncentiveType.None,
        };
        await scenario.AddAsync(employeeScenario);


        List<PayrollTemplateMonth> payrollMonthList = new List<PayrollTemplateMonth>()
        {   

            new PayrollTemplateMonth(){ Month = Months.January,WorkDay = 31,BaseSalary = 28075.50m,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.February,WorkDay = 28,BaseSalary = 28075.50m,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m }, 
            new PayrollTemplateMonth(){ Month = Months.March,WorkDay = 31,BaseSalary = 28075.50m,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m }, 
            new PayrollTemplateMonth(){ Month = Months.April,WorkDay = 30,BaseSalary = 28075.50m,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.May,WorkDay = 31,BaseSalary = 28075.50m,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.June,WorkDay = 30,BaseSalary = 28075.50m,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.July,WorkDay = 31,BaseSalary = 28075.50m,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.August,WorkDay = 31,BaseSalary = 28075.50m,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.September,WorkDay = 30,BaseSalary = 28075.50m,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.October,WorkDay = 31,BaseSalary = 28075.50m,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.November,WorkDay = 30,BaseSalary = 28075.50m,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.December,WorkDay = 31,BaseSalary = 28075.50m,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },




            // new PayrollTemplateMonth(){ Month = Months.January,WorkDay = 31,BaseSalary = 50000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            // new PayrollTemplateMonth(){ Month = Months.February,WorkDay = 28,BaseSalary = 50000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m }, 
            // new PayrollTemplateMonth(){ Month = Months.March,WorkDay = 31,BaseSalary = 50000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m }, 
            // new PayrollTemplateMonth(){ Month = Months.April,WorkDay = 30,BaseSalary = 50000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            // new PayrollTemplateMonth(){ Month = Months.May,WorkDay = 31,BaseSalary = 50000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            // new PayrollTemplateMonth(){ Month = Months.June,WorkDay = 30,BaseSalary = 50000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            // new PayrollTemplateMonth(){ Month = Months.July,WorkDay = 31,BaseSalary = 50000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            // new PayrollTemplateMonth(){ Month = Months.August,WorkDay = 31,BaseSalary = 50000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            // new PayrollTemplateMonth(){ Month = Months.September,WorkDay = 30,BaseSalary = 50000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            // new PayrollTemplateMonth(){ Month = Months.October,WorkDay = 31,BaseSalary = 50000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            // new PayrollTemplateMonth(){ Month = Months.November,WorkDay = 30,BaseSalary = 50000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            // new PayrollTemplateMonth(){ Month = Months.December,WorkDay = 31,BaseSalary = 50000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            
            
        };

        await paryollMonth.AddAsync(payrollMonthList,employeeScenario);

        
        
        var Liste = await paryollMonth.GetAsync();

        var aListe = Liste.OrderBy(x => x.Month).ToList();
        
        
        for(int i = 0; i < aListe.Count; i++)
        {   

            if(employeeScenario.SalaryInputType == SalaryInputType.Net)
            {
                var pay = await netSalaryIteration.Iterator(aListe[i]);
                await resultPayroll.AddAsync(pay);
                
            }
            else
            {
                var pay = await resultPayrollCalc.Calc(aListe[i]);
                await resultPayroll.AddAsync(pay);
            }
            
            
        } 

    
       
    }
}


           