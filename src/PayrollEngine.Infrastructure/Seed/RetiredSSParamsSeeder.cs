using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Seed;

public class RetiredSSParamsSeeder
{
    public static void Seed(PayrollEngineDbContext context, decimal year, 
                                                            decimal employeeSSRate, 
                                                            decimal employerSSRate)
                                                            
    {
        if (context.RetiredSSParams.Any(p => p.Year == year))
        {
            return; // DB has been seeded
        }

        var retiredSSParams = new RetiredSSParams
        {
            Id = Guid.NewGuid(),
            Year = year,
            EmployeeSSRate = employeeSSRate,
            EmployerSSRate = employerSSRate
            
        };
        

        context.RetiredSSParams.Add(retiredSSParams);
        context.SaveChanges();
    }
}
