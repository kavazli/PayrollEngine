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
        var DisabilityDegree = serviceProvider.GetRequiredService<DisabilityDegreeCalc>();

        var result = serviceProvider.GetRequiredService<IResultPayrollService>();
        var result2 = serviceProvider.GetRequiredService<CumulativeIncomeTaxBaseService>();
        var result3 = serviceProvider.GetRequiredService<IEmployeeScenariosService>();
        var result4 = serviceProvider.GetRequiredService<IPayrollMonthService>();

        await result.ClearAsync();
        await result2.ClearAsync();
        await result3.ClearAsync();
        await result4.ClearAsync();
        
        

        EmployeeScenario employeeScenario = new EmployeeScenario
        {
           Year = 2026,
           SalaryInputType = SalaryInputType.Gross,
           Status = Status.Active,
           DisabilityDegree = Degree.Degree3,
           PayType = PayType.Monthly,
           IncentiveType = IncentiveType.None,
        };
        await result3.AddAsync(employeeScenario);


        List<PayrollTemplateMonth> payrollMonthList = new List<PayrollTemplateMonth>()
        {
            new PayrollTemplateMonth(){ Month = Months.January,WorkDay = 31,BaseSalary = 45000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.February,WorkDay = 28,BaseSalary = 45000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m }, 
            new PayrollTemplateMonth(){ Month = Months.March,WorkDay = 31,BaseSalary = 45000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m }, 
            new PayrollTemplateMonth(){ Month = Months.April,WorkDay = 30,BaseSalary = 45000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.May,WorkDay = 31,BaseSalary = 45000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.June,WorkDay = 30,BaseSalary = 45000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.July,WorkDay = 31,BaseSalary = 45000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.August,WorkDay = 31,BaseSalary = 45000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.September,WorkDay = 30,BaseSalary = 45000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.October,WorkDay = 31,BaseSalary = 45000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.November,WorkDay = 30,BaseSalary = 45000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            new PayrollTemplateMonth(){ Month = Months.December,WorkDay = 31,BaseSalary = 45000,SalaryIncreaseRate = 0m,Overtime50 = 0m,Overtime100 = 0m,BonusAmount = 0m,PrivateHealthInsurance = 0m,ShoppingVoucher = 0m },
            
            
        };

        await result4.AddAsync(payrollMonthList,employeeScenario);

        
        
        var Liste = await result4.GetAsync();

        var aListe = Liste.OrderBy(x => x.Month).ToList();
        
        
        for(int i = 0; i < aListe.Count; i++)
        {   
            

            ResultPayroll pay = new ResultPayroll();

            pay.Month = aListe[i].Month;
            pay.WorkDay = aListe[i].WorkDay;
            pay.CurrentSalary = aListe[i].BaseSalary;
            pay.Overtime50Amount = aListe[i].Overtime50;
            pay.Overtime100Amount = aListe[i].Overtime100;
            pay.BonusAmount = aListe[i].BonusAmount;
            pay.TotalSalary = aListe[i].GrossSalary;

            pay.GrossSalary = aListe[i].GrossSalary;
            pay.SSContributionBase = await SGKTavanKontrol.Calc(pay.GrossSalary);
            pay.EmployeeSSContributionAmount = await SGKISciKesinti.Calc(pay.SSContributionBase);
            pay.EmployeeUIContributionAmount = await SGKISciIszKesinti.Calc(pay.SSContributionBase);
            pay.IncomeTaxBase = await IncomeTaxBaseCalc.Calc(pay.GrossSalary, pay.EmployeeSSContributionAmount, pay.EmployeeUIContributionAmount);

            pay.CumulativeIncomeTaxBase = await CumulativeIncomeTaxBase.Calc(pay.Month, pay.IncomeTaxBase);
            pay.IncomeTax = await IncomeTax.Calc(pay.Month);
            pay.IncomeTaxExemption = await IncomeTaxExemption.Calc(pay.Month);
            pay.StampTax = await StampTax.Calc(pay.GrossSalary);
            pay.StampTaxExemption = await StampTaxExemption.Calc();
            pay.NetSalary = NetSalary.Calc(pay.GrossSalary, pay.EmployeeSSContributionAmount, pay.EmployeeUIContributionAmount, pay.IncomeTax, pay.StampTax);
                

            await result.AddAsync(pay);
        } 

    
       
    }
}


            // new PayrollTemplateMonth(){ Month = Months.January,WorkDay = 31,BaseSalary = 30000m,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 100m,ShoppingVoucher = 10m },
            // new PayrollTemplateMonth(){ Month = Months.February,WorkDay = 28,BaseSalary = 30000m,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 100m,ShoppingVoucher = 10m }, 
            // new PayrollTemplateMonth(){ Month = Months.March,WorkDay = 31,BaseSalary = 30000m,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 100m,ShoppingVoucher = 10m }, 
            // new PayrollTemplateMonth(){ Month = Months.April,WorkDay = 30,BaseSalary = 30000m,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 100m,ShoppingVoucher = 10m },
            // new PayrollTemplateMonth(){ Month = Months.May,WorkDay = 31,BaseSalary = 30000m,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 100m,ShoppingVoucher = 10m },
            // new PayrollTemplateMonth(){ Month = Months.June,WorkDay = 30,BaseSalary = 30000m,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 100m,ShoppingVoucher = 10m },
            // new PayrollTemplateMonth(){ Month = Months.July,WorkDay = 31,BaseSalary = 30000m,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 100m,ShoppingVoucher = 10m },
            // new PayrollTemplateMonth(){ Month = Months.August,WorkDay = 31,BaseSalary = 30000m,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 100m,ShoppingVoucher = 10m },
            // new PayrollTemplateMonth(){ Month = Months.September,WorkDay = 30,BaseSalary = 30000m,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 100m,ShoppingVoucher = 10m },
            // new PayrollTemplateMonth(){ Month = Months.October,WorkDay = 31,BaseSalary = 30000m,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 100m,ShoppingVoucher = 10m },
            // new PayrollTemplateMonth(){ Month = Months.November,WorkDay = 30,BaseSalary = 30000m,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 100m,ShoppingVoucher = 10m },
            // new PayrollTemplateMonth(){ Month = Months.December,WorkDay = 31,BaseSalary = 30000m,SalaryIncreaseRate = 0.25m,Overtime50 = 10m,Overtime100 = 10m,BonusAmount = 10000m,PrivateHealthInsurance = 100m,ShoppingVoucher = 10m },