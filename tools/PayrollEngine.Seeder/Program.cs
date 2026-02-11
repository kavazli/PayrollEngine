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

        var ProviderActive = new ActiveSSParamsProvider(context);
        var ProviderRetired = new RetiredSSParamsProvider(context);
        var IncomeTaxProvider = new IncomeTaxBracketsProvider(context);

        
       
        var parametreler = ProviderActive.GetValue(2026);

        System.Console.WriteLine(parametreler.EmployeeSSRate);
        System.Console.WriteLine(parametreler.EmployeeUIRate);
        System.Console.WriteLine(parametreler.EmployerSSRate);
        System.Console.WriteLine(parametreler.EmployerUIRate);
            
    }

}