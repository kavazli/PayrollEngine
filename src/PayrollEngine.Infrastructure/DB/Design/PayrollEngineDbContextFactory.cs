using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PayrollEngine.Infrastructure.Design;

public class PayrollEngineDbContextFactory : IDesignTimeDbContextFactory<PayrollEngineDbContext>
{
    public PayrollEngineDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<PayrollEngineDbContext>();
        optionsBuilder.UseSqlite("Data Source=PayrollEngine.db");

        return new PayrollEngineDbContext(optionsBuilder.Options);
    }
}
