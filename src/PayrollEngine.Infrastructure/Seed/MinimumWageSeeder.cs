using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Seed;

public class MinimumWageSeeder
{
    public static void Seed(PayrollEngineDbContext context, decimal year, 
                                                            decimal minimumWage,
                                                            decimal netAmount)
    {
        if (context.MinimumWages.Any(m => m.Year == year))
        {
            return; // DB has been seeded
        }

        var minWage = new MinimumWage
        {
            Id = Guid.NewGuid(),
            Year = year,
            GrossAmount = minimumWage,
            NetAmount = minimumWage
        };
        
        context.MinimumWages.Add(minWage);
        context.SaveChanges();
    }
}
