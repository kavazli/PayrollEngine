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
        
       

       


    }

}