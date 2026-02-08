using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Infrastructure;
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

        // Ensure database exists and all migrations are applied before seeding
        context.Database.Migrate();

        ActiveSSParamsSeeder.Seed(context, 2026, 0.14m, 0.01m, 0.2075m, 0.02m);
        RetiredSSParamsSeeder.Seed(context, 2026, 0.075m, 0.02475m);
        SSCeilingSeeder.Seed(context, 2026, 297270m);
        StampTaxSeeder.Seed(context, 2026, 0.00759m);
        MinimumWageSeeder.Seed(context, 2026, 33030m, 28075.50m);

        List<IncomeTaxBracket> inComeTax = new List<IncomeTaxBracket>
        {
            
            new IncomeTaxBracket{ Id = Guid.NewGuid(), Year = 2026, Bracket = 1, MinAmount = 0m, MaxAmount = 190000m, Rate = 0.15m },
            new IncomeTaxBracket{ Id = Guid.NewGuid(), Year = 2026, Bracket = 2, MinAmount = 190000.01m, MaxAmount = 400000m, Rate = 0.20m },
            new IncomeTaxBracket{ Id = Guid.NewGuid(), Year = 2026, Bracket = 3, MinAmount = 400000.01m, MaxAmount = 1500000m, Rate = 0.27m },
            new IncomeTaxBracket{ Id = Guid.NewGuid(), Year = 2026, Bracket = 4, MinAmount = 1500000.01m, MaxAmount = 5300000, Rate = 0.35m },
            new IncomeTaxBracket{ Id = Guid.NewGuid(), Year = 2026, Bracket = 5, MinAmount = 5300000.01m, MaxAmount = decimal.MaxValue, Rate = 0.40m }


        };
       
        IncomeTaxBracketSeeder.Seed(context, inComeTax);
            
    }

}