

using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;


namespace PayrollEngine.Application.Calculators;



public class DisabilityDegreeCalc
{
    
    private readonly DisabilityDegreeService _disabilityDegreeService;
    

    public DisabilityDegreeCalc(DisabilityDegreeService disabilityDegreeService )
    {
        if (disabilityDegreeService == null)
        {
            throw new ArgumentNullException(nameof(disabilityDegreeService));
        }
       
        _disabilityDegreeService = disabilityDegreeService;
    }
        


    public async Task<decimal> Calc(EmployeeScenario scenario, decimal IncomeTaxBase)
    {

        if(IncomeTaxBase < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(IncomeTaxBase), "IncomeTaxBase cannot be negative.");
        }

        if(IncomeTaxBase == 0)
        {
            return 0;
        }

        if(scenario == null)
        {
            throw new ArgumentNullException(nameof(scenario), "Employee scenario cannot be null.");
        }
        
        var disabilityDegree = await _disabilityDegreeService.GetValueAsync(scenario.Year);
        if (disabilityDegree == null)
        {
            throw new InvalidOperationException("Disability degree information not found for the given year.");
        }

        if (scenario.DisabilityDegree == null || scenario.DisabilityDegree == Degree.Normal)
        {
            return IncomeTaxBase;
        }

        foreach (var degree in disabilityDegree)
        {
            if (scenario.DisabilityDegree == degree.Degree)
            {
                return Math.Round(IncomeTaxBase - degree.Amount, 2);
            }
        }

        throw new InvalidOperationException(
            $"Disability degree '{scenario.DisabilityDegree}' not found in the database for year {scenario.Year}.");
    }
}
