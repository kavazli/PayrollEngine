using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Seed;

public class ActiveSSParamsSeeder
{
    public static void Seed(PayrollEngineDbContext context, int year, 
                                                            decimal employeeSSRate, 
                                                            decimal employeeUIRate, 
                                                            decimal employerSSRate, 
                                                            decimal employerUIRate)
    {
        if (context.ActiveSSParams.Any(p => p.Year == year))
        {
            return; // DB has been seeded
        }

        var activeSSParams = new ActiveSSParams
        {
            Id = Guid.NewGuid(),
            Year = year,
            EmployeeSSRate = employeeSSRate,
            EmployeeUIRate = employeeUIRate,
            EmployerSSRate = employerSSRate,
            EmployerUIRate = employerUIRate
        };
        

        context.ActiveSSParams.Add(activeSSParams);
        context.SaveChanges();
    }
}
