using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure;

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




    public PayrollEngineDbContext(DbContextOptions<PayrollEngineDbContext> options) : base(options)
    {
    }
}
