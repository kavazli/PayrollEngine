using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PayrollEngine.Application;
using PayrollEngine.Application.Calculators;
using PayrollEngine.Application.Services;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
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

        var sgk = serviceProvider.GetRequiredService<EmployeeSSCAmountCalc>();
        var sgkI = serviceProvider.GetRequiredService<EmployeeUICAmountCalc>();

        var result = await sgk.Calc(33030);
        var result2 = await sgkI.Calc(33030);


        System.Console.WriteLine($"Employee SSC Amount: {result}");
        System.Console.WriteLine($"Employee UIC Amount: {result2}");



        
    }
}