using Microsoft.Extensions.DependencyInjection;
using PayrollEngine.Domain.Interfaces;
using PayrollEngine.Domain.Interfaces.Templates;
using PayrollEngine.Infrastructure.Providers;
using PayrollEngine.Infrastructure.Providers.Templates;

namespace PayrollEngine.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Params Providers
        services.AddScoped<IActiveSSParamsProvider, ActiveSSParamsProvider>();
        services.AddScoped<ICumulativeIncomeTaxBaseProvider, CumulativeIncomeTaxBaseProvider>();
        services.AddScoped<IIncomeTaxBracketsProvider, IncomeTaxBracketsProvider>();
        services.AddScoped<IMinimumWageProvider, MinimumWageProvider>();
        services.AddScoped<IRetiredSSParamsProvider, RetiredSSParamsProvider>();
        services.AddScoped<ISSCeilingProvider, SSCeilingProvider>();
        services.AddScoped<IStampTaxProvider, StampTaxProvider>();

        // Template Providers
        services.AddScoped<IEmployeeScenariosProvider, EmployeeScenariosProvider>();
        services.AddScoped<IPayrollMonthsProvider, PayrollMonthsProvider>();
        services.AddScoped<IResultPayrollsProvider, ResultPayrollsProvider>();

        return services;
    }
}
