using System;
using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Infrastructure.Providers;

public class CumulativeIncomeTaxBaseProvider
{   

    private readonly PayrollEngineDbContext _context;
    public CumulativeIncomeTaxBaseProvider(PayrollEngineDbContext context)
    {   
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
        
    }

    public CumulativeIncomeTaxBase GetValue(decimal Month)
    {
        var temp = _context.CumulativeIncomeTaxBases.SingleOrDefault(item => item.Month == Month);

        if(temp == null)
        {
            throw new InvalidOperationException($"CumulativeIncomeTaxBase not found for month {Month}.");          
        }

        return temp;
    }

}
