using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Seed;

public class StampTaxSeeder
{
    public static void Seed(PayrollEngineDbContext context, decimal year, 
                                                            decimal stampTaxAmount)
    {
        if (context.StampTaxes.Any(p=> p.Year == year))
        {
            return; // Data already seeded
        }

        var stampTax = new StampTax
        {
            Id = Guid.NewGuid(),
            Year = year,
            Rate = stampTaxAmount
        };

        context.StampTaxes.Add(stampTax);
        context.SaveChanges();
    }
}
