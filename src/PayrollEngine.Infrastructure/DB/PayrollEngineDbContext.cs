using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure;


// Veritabanı işlemlerini yönetmek için kullanılan DbContext sınıfı.
// DB de yönetmek istediğimiz tüm entity'ler burada DbSet olarak tanımlanır.
public class PayrollEngineDbContext : DbContext
{   

    public DbSet<EmployeeScenario> EmployeeScenarios { get; set; }
    public DbSet<ActiveSSParams> ActiveSSParams { get; set; }
    public DbSet<CumulativeIncomeTaxBase> CumulativeIncomeTaxBases { get; set; }
    public DbSet<IncomeTaxBracket> IncomeTaxBrackets { get; set; }
    public DbSet<MinimumWage> MinimumWages { get; set; }
    public DbSet<PayrollMonth> PayrollMonths { get; set; }
    public DbSet<PayrollTemplateMonth> PayrollTemplateMonths { get; set; }
    public DbSet<ResultPayroll> ResultPayrolls { get; set; }
    public DbSet<RetiredSSParams> RetiredSSParams { get; set; }
    public DbSet<SSCeiling> SSCeilings { get; set; }
    public DbSet<StampTax> StampTaxes { get; set; }
    public DbSet<DisabilityDegree> DisabilityDegrees { get; set; }

    public DbSet<ShoppingVoucher> ShoppingVouchers { get; set; }
    public DbSet<EmployerContributions> EmployerContributions { get; set; }

    

    // DbContext'in constructor'ı, DbContextOptions parametresi alır ve base sınıfına iletir.
    // Bu, DbContext'in yapılandırılmasını sağlar ve genellikle Dependency Injection ile kullanılır.
    public PayrollEngineDbContext(DbContextOptions<PayrollEngineDbContext> options) : base(options)
    {
    }
}
