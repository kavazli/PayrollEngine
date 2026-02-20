using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PayrollEngine.Infrastructure.Design;

public class PayrollEngineDbContextFactory : IDesignTimeDbContextFactory<PayrollEngineDbContext>
{
    public PayrollEngineDbContext CreateDbContext(string[] args)
    {
        // Projenin root dizinini bul (Infrastructure klasörünün parent'ı)
        var infrastructureDir = Directory.GetCurrentDirectory();
        
        // Eğer working directory Api klasörü ise, Infrastructure'a git
        if (infrastructureDir.EndsWith("PayrollEngine.Api"))
        {
            infrastructureDir = Path.Combine(infrastructureDir, "..", "PayrollEngine.Infrastructure");
        }
        
        // Database dosyasının tam yolunu oluştur
        var dbPath = Path.Combine(infrastructureDir, "PayrollEngine.db");
        dbPath = Path.GetFullPath(dbPath); // Normalize et (Mac/Windows uyumlu)
        
        var optionsBuilder = new DbContextOptionsBuilder<PayrollEngineDbContext>();
        optionsBuilder.UseSqlite($"Data Source={dbPath}");

        return new PayrollEngineDbContext(optionsBuilder.Options);
    }
}
