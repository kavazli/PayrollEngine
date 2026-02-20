using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PayrollEngine.Application.Services;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces;
using PayrollEngine.Domain.Interfaces.Templates;
using PayrollEngine.Infrastructure;
using PayrollEngine.Infrastructure.Providers;
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

        
        
       ActiveSSParamsService activeSSParamsService = new ActiveSSParamsService(new ActiveSSParamsProvider(context));
       CumulativeIncomeTaxBaseService cumulativeIncomeTaxBaseService = new CumulativeIncomeTaxBaseService(new CumulativeIncomeTaxBaseProvider(context));
       IncomeTaxBracketsService incomeTaxBracketsService = new IncomeTaxBracketsService(new IncomeTaxBracketsProvider(context));
       MinimumWageService minimumWageService = new MinimumWageService(new MinimumWageProvider(context));
       RetiredSSParamsService retiredSSParamsService = new RetiredSSParamsService(new RetiredSSParamsProvider(context));
       SSCeilingService sSCeilingService = new SSCeilingService(new SSCeilingProvider(context));
       StampTaxService stampTaxService = new StampTaxService(new StampTaxProvider(context));
       EmployeeScenariosService employeeScenariosService = new EmployeeScenariosService(new EmployeeScenariosProvider(context));
       PayrollMonthService payrollMonthService = new PayrollMonthService(new PayrollMonthsProvider(context));
       ResultPayrollService resultPayrollService = new ResultPayrollService(new ResultPayrollsProvider(context));

       /*
        var result1 = await activeSSParamsService.GetValueAsync(2026);

        System.Console.WriteLine($"ActiveSSParams for 2026: {result1.EmployeeSSRate}");
        System.Console.WriteLine($"ActiveSSParams for 2026: {result1.EmployerSSRate}");
        System.Console.WriteLine($"ActiveSSParams for 2026: {result1.EmployeeUIRate}");
        System.Console.WriteLine($"ActiveSSParams for 2026: {result1.EmployerUIRate}");
        */

        /*
        var result2 = await incomeTaxBracketsService.GetValueAsync(2026);

        foreach (var bracket in result2)
        {
            System.Console.WriteLine($"Income Tax Bracket for 2026: {bracket.MinAmount} - {bracket.MaxAmount}, Rate: {bracket.Rate}");
        }
        */

        /*
        var result3 = await minimumWageService.GetValueAsync(2026);

        System.Console.WriteLine($"Minimum Wage for 2026: {result3.GrossAmount}");
        System.Console.WriteLine($"Minimum Wage for 2026: {result3.NetAmount}");
        */

        /*
        var result4 = await retiredSSParamsService.GetValueAsync(2026);

        System.Console.WriteLine($"RetiredSSParams for 2026: {result4.EmployeeSSRate}");
        System.Console.WriteLine($"RetiredSSParams for 2026: {result4.EmployerSSRate}");
        */

        /*
        var result5 = await sSCeilingService.GetValueAsync(2026);

        System.Console.WriteLine($"SS Ceiling for 2026: {result5.Ceiling}");
        */


        /*
        var result6 = await stampTaxService.GetValueAsync(2026);

        System.Console.WriteLine($"Stamp Tax for 2026: {result6.Rate}");
        */

        
        //  var scenario = await employeeScenariosService.GetAsync();

        //     if(scenario != null)
        //     {
        //         System.Console.WriteLine($"Employee Scenario: {scenario.Id}");
        //         System.Console.WriteLine($"Employee Scenario: {scenario.SalaryInputType}");
        //         System.Console.WriteLine($"Employee Scenario: {scenario.DisabilityDegree}");
        //         System.Console.WriteLine($"Employee Scenario: {scenario.PayType}");
        //         System.Console.WriteLine($"Employee Scenario: {scenario.Status}");
        //         System.Console.WriteLine($"Employee Scenario: {scenario.IncentiveType}");
        //         System.Console.WriteLine($"Employee Scenario: {scenario.Year}");
        //     }
        //     else
        //     {
        //         System.Console.WriteLine("No Employee Scenario found.");
        //     }
        

        
        // List<PayrollMonth> months = await payrollMonthService.GetAsync();

        // foreach (var month in months)
        // {
        //     System.Console.WriteLine($"Payroll Month: {month.Id} - {month.Month} - {month.BaseSalary} - {month.GrossSalary} - {month.Overtime50}");
        // }
        

    }

}