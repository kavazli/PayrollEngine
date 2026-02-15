using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Seed;

public class SSCeilingSeeder
{
    public static void Seed(PayrollEngineDbContext context, int year, 
                                                            decimal ceilingAmount)
    {
        if (context.SSCeilings.Any(p=> p.Year == year))
        {
            return; // Data already seeded
        }

        var sscCeilings = new SSCeiling
        {
            Id = Guid.NewGuid(),
            Year = year,
            Ceiling = ceilingAmount
        };

        context.SSCeilings.Add(sscCeilings);
        context.SaveChanges();
    }
}
