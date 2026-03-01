using Microsoft.Extensions.DependencyInjection;
using PayrollEngine.Application.Calculators;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Application.Services.Services;
using PayrollEngine.Domain.Interfaces.Services;


namespace PayrollEngine.Application;


// Bu sınıf, uygulamanın uygulama katmanında kullanılan bağımlılıkları yapılandırmak için kullanılır.
// Bu sınıf, uygulamanın diğer katmanlarında kullanılan servislerin, hesaplayıcıların ve diğer uygulama hizmetlerinin bağımlılıklarını kaydeder.
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IEmployeeScenariosService, EmployeeScenariosService>();
        services.AddScoped<IPayrollMonthService, PayrollMonthService>();
        services.AddScoped<IResultPayrollService, ResultPayrollService>();
        services.AddScoped<IEmployerContributionsService, EmployerContributionsService>();
        services.AddScoped<IPrivateHealthInsuranceService, PrivateHealthInsuranceService>();
        services.AddScoped<IShoppingVoucherService, ShoppingVoucherService>();
        

        // Param Services
        services.AddScoped<ActiveSSParamsService>();
        services.AddScoped<CumulativeIncomeTaxBaseService>();
        services.AddScoped<IncomeTaxBracketsService>();
        services.AddScoped<MinimumWageService>();
        services.AddScoped<RetiredSSParamsService>();
        services.AddScoped<SSCeilingService>();
        services.AddScoped<StampTaxService>();
        services.AddScoped<DisabilityDegreeService>();

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
        services.AddScoped<DisabilityDegreeCalc>();
        services.AddScoped<ResultPayrollCalc>();


        services.AddScoped<NetSalaryIteration>();

        return services;
    }
}
