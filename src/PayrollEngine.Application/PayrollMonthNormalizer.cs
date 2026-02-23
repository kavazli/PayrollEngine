

using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Application;


// UI kullancısının girdiği verileri, hesaplamalarda kullanılacak verilere dönüştürmek için kullanılan sınıf.
// Bu sınıf, PayrollTemplateMonth ve EmployeeScenario nesnelerini alır ve PayrollMonth nesnesine dönüştürür.
public class PayrollMonthNormalizer
{
    private PayrollTemplateMonth TemplateMonth { get; set; }
    private EmployeeScenario Scenario { get; set; }

    public PayrollMonthNormalizer(PayrollTemplateMonth templateMonth, EmployeeScenario scenario)
    {   
        if(templateMonth == null)
        {
            throw new ArgumentNullException(nameof(templateMonth),"cannot be null.");
        }
        if(scenario == null)
        {
            throw new ArgumentNullException(nameof(scenario),"cannot be null.");
        }
        TemplateMonth = templateMonth;
        Scenario = scenario;
    }


    // Normalize yöntemi, PayrollTemplateMonth ve 
    // EmployeeScenario nesnelerini kullanarak PayrollMonth nesnesini oluşturur.
    public PayrollMonth Normalize()
    {   
        var TempWorkDay = CalculateWorkDays();
        var TempBaseSalary = CalculateSalaryIncrease();
        var TempOvertime50 = CalculateOvertime50(TempBaseSalary);
        var TempOvertime100 = CalculateOvertime100(TempBaseSalary);
        var TempGrossSalary = TempBaseSalary + TempOvertime50 + TempOvertime100 + TemplateMonth.BonusAmount;


        PayrollMonth payrollMonth = new PayrollMonth()
        {   
              

            Month = TemplateMonth.Month,
            WorkDay = TempWorkDay,
            BaseSalary = TempBaseSalary,
            Overtime50 = TempOvertime50,
            Overtime100 = TempOvertime100,
            BonusAmount = TemplateMonth.BonusAmount,
            GrossSalary = TempGrossSalary
        };

        return payrollMonth;
    }


    // Maaş artışını hesaplayan yöntem.
    private decimal CalculateSalaryIncrease()
    {   
        var result = TemplateMonth.BaseSalary * TemplateMonth.SalaryIncreaseRate;

        return TemplateMonth.BaseSalary + result;
    }


    // 50% fazla mesai ücretini hesaplayan yöntem.
    private decimal CalculateOvertime50(decimal BaseSalary)
    {         
       return TemplateMonth.Overtime50 * (BaseSalary / 225m) * 1.5m;
    }


    // 100% fazla mesai ücretini hesaplayan yöntem.
    private decimal CalculateOvertime100(decimal BaseSalary)
    {         
       return TemplateMonth.Overtime100 * (BaseSalary / 225m) * 2m;
    }


    // Çalışma günlerini hesaplayan yöntem. 
    // Bu yöntem, ödeme türüne (günlük veya aylık) ve aya göre çalışma günlerini belirler.
    private int CalculateWorkDays()
    {   
        int tempDay;

        if(Scenario.PayType == Domain.Enums.PayType.Daily)
        {   
            switch (TemplateMonth.Month)
            {   case Domain.Enums.Months.January:
                    tempDay = (int)Domain.Enums.MonthsDay.January;
                    break;
                case Domain.Enums.Months.February:
                    tempDay = (int)Domain.Enums.MonthsDay.February;
                    break;
                case Domain.Enums.Months.March:
                    tempDay = (int)Domain.Enums.MonthsDay.March;
                    break;
                case Domain.Enums.Months.April:
                    tempDay = (int)Domain.Enums.MonthsDay.April;
                    break;
                case Domain.Enums.Months.May:
                    tempDay = (int)Domain.Enums.MonthsDay.May;
                    break;
                case Domain.Enums.Months.June:
                    tempDay = (int)Domain.Enums.MonthsDay.June;
                    break;
                case Domain.Enums.Months.July:
                    tempDay = (int)Domain.Enums.MonthsDay.July;
                    break;
                case Domain.Enums.Months.August:
                    tempDay = (int)Domain.Enums.MonthsDay.August;
                    break;
                case Domain.Enums.Months.September:
                    tempDay = (int)Domain.Enums.MonthsDay.September;
                    break;
                case Domain.Enums.Months.October:
                    tempDay = (int)Domain.Enums.MonthsDay.October;
                    break;
                case Domain.Enums.Months.November:
                    tempDay = (int)Domain.Enums.MonthsDay.November;
                    break;
                case Domain.Enums.Months.December:        
                tempDay = (int)Domain.Enums.MonthsDay.December;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(" one error occurred. Invalid month value.");

                
            }
            
             return tempDay;     
        }
        else if(Scenario.PayType == Domain.Enums.PayType.Monthly)
        {
            return 30;
        }
        else
        {
            throw new ArgumentOutOfRangeException(" one error occurred. Invalid pay type.");
        }
        
    }

}
