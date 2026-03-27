using System.Net.Http.Json;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Ui.Pages;

public partial class Payroll
{

    // Ocak ayındaki veriler değiştiğinde diğer 11 aya kopyalamak için method
    private void OnJanuaryDataChanged()
    {
        if (_templateMonths.Count < 12) return;
        
        var january = _templateMonths[0]; // Ocak
        
        // Ocak'tan diğer 11 aya kopyala (WorkDay HARİÇ)
        for (int i = 1; i < 12; i++)
        {
            _templateMonths[i].BaseSalary = january.BaseSalary;
            _templateMonths[i].SalaryIncreaseRate = january.SalaryIncreaseRate;
            _templateMonths[i].Overtime50 = january.Overtime50;
            _templateMonths[i].Overtime100 = january.Overtime100;
            _templateMonths[i].BonusAmount = january.BonusAmount;
            _templateMonths[i].ShoppingVoucher = january.ShoppingVoucher;
        }
    }


    // Çalışan Senaryosu seçildikten sonra DB ye kaydetmek için buton üzerine tıklanınca çalışacak method
    private async Task SaveScenario()
    {
        var scenario = new EmployeeScenario
        {
            Year             = _year,
            SalaryInputType  = Enum.Parse<SalaryInputType>(_salaryType),
            Status           = Enum.Parse<Status>(_status),
            DisabilityDegree = Enum.Parse<Degree>(_disabilityDegree),
            PayType          = Enum.Parse<PayType>(_payType),
            Sector           = Enum.Parse<Sector>(_sector),
            IncentiveType    = Enum.Parse<IncentiveType>(_incentiveType)
        };

        var response = await Http.PostAsJsonAsync("api/employeescenarios", scenario);

        if (response.IsSuccessStatusCode)
        {
            _ScenarioMessage = "Senaryo kaydedildi!";
            
            // await Task.Delay(2000);
            
            // _ScenarioMessage = string.Empty; // Mesajı temizle

        }
    }


    // 12 aylık maaş verilerini DB'ye kaydetmek için buton üzerine tıklanınca çalışacak method
    private async Task SaveMonths()
    {
        // API'ye direkt PayrollTemplateMonth listesini gönder
        var response = await Http.PostAsJsonAsync("api/payrollmonth", _templateMonths);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("12 ay kaydedildi ve bordro hesaplandı!");
        }
        else
        {
            Console.WriteLine($"Hata: {response.StatusCode}");
        }
    }
}
