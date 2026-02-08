using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Seed;

public class IncomeTaxBracketSeeder
{
    public static void Seed(PayrollEngineDbContext context, List<IncomeTaxBracket> taxBrackets)
    {
        if (context.IncomeTaxBrackets.Any(b => b.Year == taxBrackets.First().Year))
        {
            return; // DB has been seeded
        }
        
        context.IncomeTaxBrackets.AddRange(taxBrackets);
        context.SaveChanges();
    }
     
}
