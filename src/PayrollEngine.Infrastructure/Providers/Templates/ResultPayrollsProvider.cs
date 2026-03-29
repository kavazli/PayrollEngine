using System;
using Microsoft.EntityFrameworkCore;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;
using PayrollEngine.Domain.Interfaces.Providers;


namespace PayrollEngine.Infrastructure.Providers.Templates;


// Hesaplamalardan dönen sonuçların veritabanında tutulması ve yönetilmesi için oluşturulan provider.
public class ResultPayrollsProvider : IResultPayrollsProvider
{
    private readonly PayrollEngineDbContext _context;

    public ResultPayrollsProvider(PayrollEngineDbContext context)
    {

        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        _context = context;
        
    }


    // Veritabanına yeni bir ResultPayroll nesnesi ekler.
    // Eğer null bir nesne gönderilirse, ArgumentNullException fırlatır.
    public async Task<ResultPayroll> AddAsync(ResultPayroll resultPayroll)
    {
        if (resultPayroll == null)
        {
            throw new ArgumentNullException(nameof(resultPayroll));
        }

        await _context.ResultPayrolls.AddAsync(resultPayroll);
        await _context.SaveChangesAsync();
        return resultPayroll;
    }


    // Veritabanına birden fazla ResultPayroll nesnesi ekler.
    // Eğer null bir liste gönderilirse, ArgumentNullException fırlatır.
    public async Task<List<ResultPayroll>> AddRangeAsync(List<ResultPayroll> resultPayrolls)
    {
        if (resultPayrolls == null)
        {
            throw new ArgumentNullException(nameof(resultPayrolls));
        }

        await _context.ResultPayrolls.AddRangeAsync(resultPayrolls);
        await _context.SaveChangesAsync();
        return resultPayrolls;
    }


    // Veritabanındaki tüm ResultPayroll nesnelerini döndürür.
    // Eğer veritabanında hiç ResultPayroll yoksa, hata fırlatır.
    public async Task<List<ResultPayroll>> GetAsync()
    {   
        var resultPayrolls = await _context.ResultPayrolls.OrderBy(x => x.Month).ToListAsync();
        
        return resultPayrolls;
    }


    // Veritabanından belirli bir aya ait ResultPayroll nesnesini döndürür.
    // Eğer belirtilen aya ait bir ResultPayroll bulunamazsa, hata fırlatır
    public async Task<ResultPayroll> GetMonthAsync(Months month)
    {
        var resultPayroll = await _context.ResultPayrolls.FirstOrDefaultAsync(rp => rp.Month == month);
        if (resultPayroll == null)
        {
            throw new InvalidOperationException($"No result payroll found for month {month}.");
        }

        return resultPayroll;
    }


    // Veritabanındaki tüm ResultPayroll nesnelerini temizler.
    public async Task ClearAsync()
    {
        var resultPayrolls = await _context.ResultPayrolls.ToListAsync();
        _context.ResultPayrolls.RemoveRange(resultPayrolls);
        await _context.SaveChangesAsync();
    }


    // Veritabanındaki ResultPayroll nesnelerini temizler ve ardından,
    // yeni bir ResultPayroll nesnesi ekler.
    public async Task SetAsync(List<ResultPayroll> resultPayrolls)
    {
        if (resultPayrolls == null)
        {
            throw new ArgumentNullException(nameof(resultPayrolls));
        }


        // Veritabanında birden fazla ResultPayroll nesnesi olabileceği için,
        // İşlem açıp kapatmak daha güvenli olacaktır. Böylece,
        // herhangi bir hata durumunda, veritabanı tutarsız kalmaz.
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var existingResultPayrolls = await _context.ResultPayrolls.ToListAsync();
            if(existingResultPayrolls == null || existingResultPayrolls.Count == 0)
            {
                throw new InvalidOperationException("No result payrolls found in the database to clear.");
            }

            _context.ResultPayrolls.RemoveRange(existingResultPayrolls);

            await _context.ResultPayrolls.AddRangeAsync(resultPayrolls);
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
