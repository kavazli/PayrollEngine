using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Ui.Pages;

public partial class Payroll
{
    [Inject]
    public HttpClient Http { get; set; } = null!;

    private int _year = 2026;
    private string _salaryType = "Gross";
    private string _status = "Active";
    private string _disabilityDegree = "Normal";
    private string _payType = "Monthly";
    private string _sector = "Manufacturing";
    private string _incentiveType = "None";


    private static readonly Dictionary<Months, string> MonthNames = new()
    {
        { Months.January, "Ocak" },
        { Months.February, "Şubat" },
        { Months.March, "Mart" },
        { Months.April, "Nisan" },
        { Months.May, "Mayıs" },
        { Months.June, "Haziran" },
        { Months.July, "Temmuz" },
        { Months.August, "Ağustos" },
        { Months.September, "Eylül" },
        { Months.October, "Ekim" },
        { Months.November, "Kasım" },
        { Months.December, "Aralık" }
    };





    private record ResultRow(
        string MonthName,
        int WorkDay, int SSDay,
        decimal CurrentSalary, decimal Overtime50Amount, decimal Overtime100Amount,
        decimal BonusAmount, decimal TotalSalary,
        decimal GrossSalary, decimal SSContributionBase,
        decimal EmployeeSSC, decimal EmployeeUIC,
        decimal IncomeTaxBase, decimal CumulativeIncomeTaxBase,
        decimal IncomeTax, decimal IncomeTaxRate, decimal IncomeTaxExemption,
        decimal StampTax, decimal StampTaxExemption,
        decimal NetSalary);

    
    private List<ResultRow> _results = new();



    private List<PayrollTemplateMonth> _templateMonths = InitializeMonths();

    private static List<PayrollTemplateMonth> InitializeMonths()
    {
        var list = new List<PayrollTemplateMonth>();
        
        foreach (Months month in Enum.GetValues<Months>())
        {
            list.Add(new PayrollTemplateMonth { Month = month });
        }
        
        return list;
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        UpdateWorkDays();
    }

    private void OnPayTypeChanged()
    {
        UpdateWorkDays();
    }

    private void OnYearChanged()
    {
        if (_payType == "Daily")
        {
            UpdateWorkDays();
        }
    }

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
            Console.WriteLine("Kaydedildi!");
        }
    }



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
