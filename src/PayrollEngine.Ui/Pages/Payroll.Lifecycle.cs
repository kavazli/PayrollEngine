using Microsoft.AspNetCore.Components;

namespace PayrollEngine.Ui.Pages;

public partial class Payroll
{   

    // Sayfa açıldığında veya yıl/pay type değiştiğinde çalışacak method, çalışma günlerini güncellemek için
    protected override void OnInitialized()
    {
        base.OnInitialized();
        UpdateWorkDays();
    }
}
