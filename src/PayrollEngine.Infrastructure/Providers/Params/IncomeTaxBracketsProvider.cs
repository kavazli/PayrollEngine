using System;
using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Infrastructure.Providers;

public class IncomeTaxBracketsProvider
{
    readonly private  PayrollEngineDbContext _context;

    public IncomeTaxBracketsProvider(PayrollEngineDbContext context)
    {
        if(context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        _context = context;
    }

    public List<IncomeTaxBracket> GetValues(decimal year)
    {
        var temp = _context.IncomeTaxBrackets.Where(b => b.Year == year).OrderBy(b => b.MinAmount).ToList();

        if(temp == null || temp.Count == 0)
        {
            throw new InvalidOperationException($"No IncomeTaxBrackets found for year {year}.");          
        }

        return temp;
    }

}
