
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Providers;


namespace PayrollEngine.Infrastructure.Providers.Templates;


// hesaplamalardan dönen sonuçların veritabanında tutulması ve yönetilmesi için oluşturulan provider.
public class PayrollMonthsProvider : IPayrollMonthsProvider
{

    private readonly PayrollEngineDbContext _context;

    public PayrollMonthsProvider(PayrollEngineDbContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        _context = context;
    }


    // Veritabanına birden fazla PayrollMonth nesnesi ekler.
    // Eğer null bir liste gönderilirse, ArgumentNullException fırlatır.
    public async Task<List<PayrollMonth>> AddAsync(List<PayrollMonth> months)
    {
        if (months == null)
        {
            throw new ArgumentNullException(nameof(months));
        }

        await _context.PayrollMonths.AddRangeAsync(months);
        await _context.SaveChangesAsync();
        return months;
    }
    

    // Veritabanındaki tüm PayrollMonth nesnelerini döndürür.
    // Eğer veritabanında hiç PayrollMonth yoksa, hata fırlatır.
    public async Task<List<PayrollMonth>> GetAsync()
    {   

        var payrollMonth = await _context.PayrollMonths.ToListAsync();
        if(payrollMonth == null)
        {
            throw new InvalidOperationException("No payroll months found in the database.");
        }

        return payrollMonth;
    }


    // Veritabanındaki tüm PayrollMonth nesnelerini temizler.
    public async Task ClearAsync()
    {
        var months = await _context.PayrollMonths.ToListAsync();
        _context.PayrollMonths.RemoveRange(months);
        await _context.SaveChangesAsync();
    }
    

    // Veritabanındaki PayrollMonth nesnelerini temizler ve ardından,
    // yeni bir PayrollMonth nesnesi ekler.
    public async Task SetAsync(List<PayrollMonth> months)
    {
        if (months == null)
        {
            throw new ArgumentNullException(nameof(months));
        }


        // Veritabanında birden fazla PayrollMonth nesnesi olabileceği için,
        // İşlem açıp kapatmak daha güvenli olacaktır. Böylece,
        // herhangi bir hata durumunda, veritabanı tutarsız kalmaz.
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var existingMonths = await _context.PayrollMonths.ToListAsync();
            if (existingMonths == null || existingMonths.Count == 0)
            {
                throw new InvalidOperationException("No payroll months found in the database to clear.");
            }
            _context.PayrollMonths.RemoveRange(existingMonths);
            
            await _context.PayrollMonths.AddRangeAsync(months);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

}
