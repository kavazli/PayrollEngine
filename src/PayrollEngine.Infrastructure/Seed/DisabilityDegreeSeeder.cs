using System;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Infrastructure.Seed;

public class DisabilityDegreeSeeder
{

        public DisabilityDegreeSeeder(PayrollEngineDbContext context, 
                                      int year, 
                                      Degree disabilityDegree, 
                                      decimal amount)
        {

            if (context.DisabilityDegrees.Any(d => d.Year == year && d.Degree == disabilityDegree))
            {
                return; // DB has been seeded
            }

            var disabilityDegreeEntity = new DisabilityDegree
            {
                Id = Guid.NewGuid(),
                Year = year,
                Degree = disabilityDegree,
                Amount = amount
            };

            context.DisabilityDegrees.Add(disabilityDegreeEntity);
            context.SaveChanges();
        }
}
