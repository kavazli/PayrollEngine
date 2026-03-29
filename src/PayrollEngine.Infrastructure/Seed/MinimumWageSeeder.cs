using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Seed;

public class MinimumWageSeeder
{
    public static void Seed(PayrollEngineDbContext context, int year,
                                                            decimal minimumWage,
                                                            decimal netAmount,
                                                            decimal retiredNetAmount)
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
            NetAmount = netAmount,
            RetiredNetAmount = retiredNetAmount
        };
        
        context.MinimumWages.Add(minWage);
        context.SaveChanges();
    }
}
