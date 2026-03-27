namespace PayrollEngine.Ui.Pages;

public partial class Payroll
{   

    // Sayfa açıldığında veya yıl/pay type değiştiğinde çalışacak method, çalışma günlerini güncellemek için
    private void OnPayTypeChanged()
    {
        UpdateWorkDays();
    }

    // Yıl değiştiğinde çalışacak method, çalışma günlerini güncellemek için
    private void OnYearChanged()
    {
        if (_payType == "Daily")
        {
            UpdateWorkDays();
        }
    }

    // Çalışma günlerini güncellemek için method
    private void UpdateWorkDays()
    {
        if (_payType == "Monthly")
        {
            // Tüm ayları 30 gün yap
            foreach (var month in _templateMonths)
            {
                month.WorkDay = 30;
            }
        }
        else // Daily
        {
            // Her ayın gerçek gün sayısını hesapla
            foreach (var month in _templateMonths)
            {
                int daysInMonth = DateTime.DaysInMonth(_year, (int)month.Month);
                month.WorkDay = daysInMonth;
            }
        }
    }
}
