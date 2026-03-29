using System.Net.Http.Json;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Ui.Pages;

public partial class Payroll
{

    // Aylık maaş verilerini sıfırlamak için buton üzerine tıklanınca çalışacak method
    private void ClearMonths()
    {
        _templateMonths = InitializeMonths();
        _results = new List<ResultPayroll>();
        _employerContributions = new List<EmployerContributions>();
    }


    // Asgari ücret verilerini API'den yükler
    private async Task LoadMinimumWageAsync()
    {
        var scenarioResponse = await Http.GetAsync("api/employeescenarios");
        if (scenarioResponse.IsSuccessStatusCode)
        {
            var scenario = await scenarioResponse.Content.ReadFromJsonAsync<EmployeeScenario>();
            if (scenario != null)
                _year = scenario.Year;
        }

        var response = await Http.GetAsync($"api/minimumwage/{_year}");
        if (response.IsSuccessStatusCode)
            _minimumWage = await response.Content.ReadFromJsonAsync<MinimumWage>();
    }

    // Ocak temel maaşının asgari ücretin altında olup olmadığını kontrol eder
    private async Task ValidateBaseSalary()
    {
        if (_minimumWage == null) return;

        var scenarioResponse = await Http.GetAsync("api/employeescenarios");
        if (!scenarioResponse.IsSuccessStatusCode) return;

        var scenario = await scenarioResponse.Content.ReadFromJsonAsync<EmployeeScenario>();
        if (scenario == null) return;

        var january = _templateMonths[0];
        decimal limit;
        string limitLabel;

        if (scenario.SalaryInputType == SalaryInputType.Gross)
        {
            limit = _minimumWage.GrossAmount;
            limitLabel = "brüt";
        }
        else if (scenario.Status == Status.Active)
        {
            limit = _minimumWage.NetAmount;
            limitLabel = "net";
        }
        else
        {
            limit = _minimumWage.RetiredNetAmount;
            limitLabel = "emekli net";
        }

        _baseSalaryWarning = january.BaseSalary > 0 && january.BaseSalary < limit
            ? $"Temel maaş asgari ücretin {limitLabel} tutarından ({limit:N2} ₺) düşük olamaz."
            : string.Empty;
    }

    // Ocak ayındaki veriler değiştiğinde diğer 11 aya kopyalamak için method
    private async Task OnJanuaryDataChanged()
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

        await ValidateBaseSalary();
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
            UpdateWorkDays();

            _ScenarioMessage = "Senaryo kaydedildi!";
            StateHasChanged();
            
            await Task.Delay(2000);
            
            _ScenarioMessage = string.Empty; // Mesajı temizle
            StateHasChanged();
        }
    }


    // 12 aylık maaş verilerini DB'ye kaydetmek için buton üzerine tıklanınca çalışacak method
    private async Task SaveMonths()
    {
        await ValidateBaseSalary();
        if (!string.IsNullOrEmpty(_baseSalaryWarning))
        {
            _ScenarioMessage = _baseSalaryWarning;
            StateHasChanged();
            return;
        }

        // API'ye direkt PayrollTemplateMonth listesini gönder
        var response = await Http.PostAsJsonAsync("api/payrollmonth", _templateMonths);

        if (response.IsSuccessStatusCode)
        {
            _ScenarioMessage = "Senaryo kaydedildi!";
            StateHasChanged();
            
            await ResultPayrolls();
            await EmployerContributions();

            await Task.Delay(2000);
            
            _ScenarioMessage = string.Empty; // Mesajı temizle
            StateHasChanged();
        }
    }

    private async Task ResultPayrolls()
    {
        var response = await Http.GetAsync("api/ResultPayroll");

        if (response.IsSuccessStatusCode)
        {
            _results = await response.Content.ReadFromJsonAsync<List<ResultPayroll>>() ?? new List<ResultPayroll>();
            StateHasChanged();
        }
    }

    private async Task EmployerContributions()
    {
        var response = await Http.GetAsync("api/EmployerContributions");

        if (response.IsSuccessStatusCode)
        {
            _employerContributions = await response.Content.ReadFromJsonAsync<List<EmployerContributions>>() ?? new List<EmployerContributions>();
            StateHasChanged();
        }
    }


}
