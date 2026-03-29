

using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Enums;


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
        var TempBaseSalary = CalculateSalaryIncrease(TempWorkDay);
        var TempOvertime50 = CalculateOvertime50(TempBaseSalary, TempWorkDay);
        var TempOvertime100 = CalculateOvertime100(TempBaseSalary, TempWorkDay);
        decimal resultSalary = TempBaseSalary + TempOvertime50 + TempOvertime100 + TemplateMonth.BonusAmount;
        var TempGrossSalary = Math.Round(resultSalary, 2);
        var TempShoppingVoucher = TemplateMonth.ShoppingVoucher;


        PayrollMonth payrollMonth = new PayrollMonth()
        {   
              

            Month = TemplateMonth.Month,
            WorkDay = TempWorkDay,
            BaseSalary = TempBaseSalary,
            Overtime50 = TempOvertime50,
            Overtime100 = TempOvertime100,
            BonusAmount = TemplateMonth.BonusAmount,
            GrossSalary = TempGrossSalary,
            ShoppingVoucher = TempShoppingVoucher
        };

        return payrollMonth;
    }


    // Maaş artışını hesaplayan yöntem.
    private decimal CalculateSalaryIncrease(int workDay)
    {   
        if(TemplateMonth.SalaryIncreaseRate == 0)
        {   
            var reslut = CalculateSalary(workDay, TemplateMonth.BaseSalary);

            return Math.Round(reslut, 2);
        }

        var result = TemplateMonth.BaseSalary + (TemplateMonth.BaseSalary * (TemplateMonth.SalaryIncreaseRate / 100));

        return Math.Round(CalculateSalary(workDay, result), 2);
    }


    // 50% fazla mesai ücretini hesaplayan yöntem.
    private decimal CalculateOvertime50(decimal BaseSalary, int workDay)
    {  
        if (TemplateMonth.Overtime50 == 0)
        {
            return 0;
        }


       var DailySalary = BaseSalary / workDay; 
       decimal result = TemplateMonth.Overtime50 * (DailySalary / 7.5m) * 1.5m; 
       return Math.Round(result, 2);
    }


    // 100% fazla mesai ücretini hesaplayan yöntem.
    private decimal CalculateOvertime100(decimal BaseSalary, int workDay)
    {           
        if (TemplateMonth.Overtime100 == 0)
        {
            return 0;
            
        }  
       var DailySalary = BaseSalary / workDay;
       decimal result = TemplateMonth.Overtime100 * (DailySalary / 7.5m) * 2m;

       return Math.Round(result, 2);

    }


    // Maaşı güne göre hesaplayan yöntem.
    private decimal CalculateSalary(int workDay, decimal baseSalary)
    {   
        decimal result = (baseSalary / 30m) * workDay;

        return Math.Round(result, 2);
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
