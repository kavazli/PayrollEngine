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

        
       
        var result = serviceProvider.GetRequiredService<IResultPayrollService>();
        var result2 = serviceProvider.GetRequiredService<CumulativeIncomeTaxBaseService>();
        var result3 = serviceProvider.GetRequiredService<IEmployeeScenariosService>();
        var result4 = serviceProvider.GetRequiredService<IPayrollMonthService>();
        var result5 = serviceProvider.GetRequiredService<ResultPayrollCalc>();
        var result6 = serviceProvider.GetRequiredService<NetSalaryIteration>();

        await result.ClearAsync();
        await result2.ClearAsync();
        await result3.ClearAsync();
        await result4.ClearAsync();
        
        

        EmployeeScenario employeeScenario = new EmployeeScenario
        {
           Year = 2026,
           SalaryInputType = SalaryInputType.Net,
           Status = Status.Active,
           DisabilityDegree = Degree.Normal,
           PayType = PayType.Monthly,
           IncentiveType = IncentiveType.None,
        };
        await result3.AddAsync(employeeScenario);


        List<PayrollTemplateMonth> payrollMonthList = new List<PayrollTemplateMonth>()
        {   

            new PayrollTemplateMonth(){ Month = Months.January,WorkDay = 31,BaseSalary = 1000000,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.February,WorkDay = 28,BaseSalary = 1000000,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m }, 
            new PayrollTemplateMonth(){ Month = Months.March,WorkDay = 31,BaseSalary = 1000000,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m }, 
            new PayrollTemplateMonth(){ Month = Months.April,WorkDay = 30,BaseSalary = 1000000,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.May,WorkDay = 31,BaseSalary = 1000000,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.June,WorkDay = 30,BaseSalary = 1000000,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.July,WorkDay = 31,BaseSalary = 1000000,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.August,WorkDay = 31,BaseSalary = 1000000,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.September,WorkDay = 30,BaseSalary = 1000000,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.October,WorkDay = 31,BaseSalary = 1000000,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.November,WorkDay = 30,BaseSalary = 1000000,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.December,WorkDay = 31,BaseSalary = 1000000,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },




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

        await result4.AddAsync(payrollMonthList,employeeScenario);

        
        
        var Liste = await result4.GetAsync();

        var aListe = Liste.OrderBy(x => x.Month).ToList();
        
        
        for(int i = 0; i < aListe.Count; i++)
        {   

            if(employeeScenario.SalaryInputType == SalaryInputType.Net)
            {
                var pay = await result6.Iterator(aListe[i]);
                await result.AddAsync(pay);
                
            }
            else
            {
                var pay = await result5.Calc(aListe[i]);
                await result.AddAsync(pay);
            }
            
            
        } 

    
       
    }
}


           