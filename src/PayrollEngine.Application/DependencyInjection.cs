using Microsoft.Extensions.DependencyInjection;
using PayrollEngine.Application.Calculators;
using PayrollEngine.Application.Services;
using PayrollEngine.Application.Services.Params;

namespace PayrollEngine.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Services
        services.AddScoped<EmployeeScenariosService>();
        services.AddScoped<PayrollMonthService>();
        services.AddScoped<ResultPayrollService>();

        // Param Services
        services.AddScoped<ActiveSSParamsService>();
        services.AddScoped<CumulativeIncomeTaxBaseService>();
        services.AddScoped<IncomeTaxBracketsService>();
        services.AddScoped<MinimumWageService>();
        services.AddScoped<RetiredSSParamsService>();
        services.AddScoped<SSCeilingService>();
        services.AddScoped<StampTaxService>();

        // Calculators
        services.AddScoped<CumtIncomeTaxBaseCalc>();
        services.AddScoped<EmployeeSSCAmountCalc>();
        services.AddScoped<EmployeeUICAmountCalc>();
        services.AddScoped<IncomeTaxBaseCalc>();
        services.AddScoped<IncomeTaxCalc>();
        services.AddScoped<IncomeTaxExemptionCalc>();
        services.AddScoped<SSContributionCalc>();
        services.AddScoped<NetSalaryCalc>();
        services.AddScoped<StampTaxCalc>();
        services.AddScoped<StampTaxExemptionCalc>();

        return services;
    }
}
