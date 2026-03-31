using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;

namespace PayrollEngine.Ui.Pages;

public partial class Payroll
{   
    // HttpClient'i dependency injection ile alıyoruz, API çağrıları için kullanacağız
    [Inject]
    public HttpClient Http { get; set; } = null!;

    private string ExportUrl => $"{Http.BaseAddress}api/ResultPayroll/export";

    private int _year = 2026;
    private string _salaryType = "Gross";
    private string _status = "Active";
    private string _disabilityDegree = "Normal";
    private string _payType = "Monthly";
    private string _sector = "Manufacturing";
    private string _incentiveType = "None";
    private string _scenarioMessage = string.Empty; // Senaryo kaydedildikten sonra gösterilecek mesaj için
    private string _payrollMonthMessage = string.Empty; // Maaş hesaplama sonrası gösterilecek mesaj için

    private MinimumWage? _minimumWage;
    private string _baseSalaryWarning = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await LoadMinimumWageAsync();
    }


    
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



    List<ResultPayroll> _results = new List<ResultPayroll>();
    List<EmployerContributions> _employerContributions = new List<EmployerContributions>();


    private List<PayrollTemplateMonth> _templateMonths = InitializeMonths();

    // şablon olarak kullanılacak aylık veriler, başlangıçta sadece ay bilgisi dolu, diğer alanlar kullanıcı tarafından doldurulacak
    private static List<PayrollTemplateMonth> InitializeMonths()
    {
        var list = new List<PayrollTemplateMonth>();
        
        foreach (Months month in Enum.GetValues<Months>())
        {
            list.Add(new PayrollTemplateMonth { Month = month });
        }
        
        return list;
    }
}
