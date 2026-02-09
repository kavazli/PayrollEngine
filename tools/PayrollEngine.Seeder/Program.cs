using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Infrastructure;
using PayrollEngine.Infrastructure.Providers;
using PayrollEngine.Infrastructure.Seed;


class Program
{
    static void Main(string[] args)
    {   



        
        var options = new DbContextOptionsBuilder<PayrollEngineDbContext>()
            .UseSqlite(
                "Data Source=../../src/PayrollEngine.Infrastructure/PayrollEngine.db")
            .Options;

        using var context = new PayrollEngineDbContext(options);

        var Provider = new ActiveSSParamsProvider(context);
       
        var parametreler = Provider.GetValue(2026);

        System.Console.WriteLine(parametreler.EmployeeSSRate);
        System.Console.WriteLine(parametreler.EmployeeUIRate);
        System.Console.WriteLine(parametreler.EmployerSSRate);
        System.Console.WriteLine(parametreler.EmployerUIRate);;
            
    }

}