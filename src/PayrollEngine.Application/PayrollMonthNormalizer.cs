using System;

using PayrollEngine.Domain.Entities;

namespace PayrollEngine.Application;

public class PayrollMonthNormalizer
{
    private PayrollTemplateMonth TemplateMonth { get; set; }
    private EmployeeScenario Scenario { get; set; }

    public PayrollMonthNormalizer(PayrollTemplateMonth templateMonth, EmployeeScenario scenario)
    {   
        if(templateMonth == null)
        {
            throw new ArgumentNullException(nameof(templateMonth));
        }
        if(scenario == null)
        {
            throw new ArgumentNullException(nameof(scenario));
        }
        TemplateMonth = templateMonth;
        Scenario = scenario;
    }

    public PayrollMonth Normalize()
    {
        

        return new PayrollMonth
        {
            Month = TemplateMonth.Month,
            WorkDay = CalculateWorkDays(),
            BaseSalary = CalculateSalaryIncrease(),
            
            
            
        };
    }

    private decimal CalculateSalaryIncrease()
    {
        return TemplateMonth.BaseSalary * TemplateMonth.SalaryIncreaseRate;
    }


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
                    throw new ArgumentOutOfRangeException("Bir hata oluştu. Geçersiz ay değeri.");

                
            }
            
             return tempDay;     
        }
        else if(Scenario.PayType == Domain.Enums.PayType.Monthly)
        {
            return 30;
        }
        else
        {
            throw new ArgumentOutOfRangeException("Bir hata oluştu. Geçersiz ödeme tipi.");
        }
        
    }

}
