using System;

namespace PayrollEngine.Domain.Entitys;

public class IncomeTaxBrackets
{
    
    private readonly List<IncomeTaxBracket> _brackets;


    // Vergi dilimlerinin okunabilir listesi
    public IReadOnlyList<IncomeTaxBracket> Brackets => _brackets;


    // Yapıcı metot, vergi dilimlerini alır ve doğrular, sıralar, çakışmaları kontrol eder
    public IncomeTaxBrackets(IEnumerable<IncomeTaxBracket> brackets)
    {   

        if (brackets == null || !brackets.Any())
        {
            throw new ArgumentException("At least one tax bracket must be provided.");
        }
            

        // 1. MinAmount'a göre sırala
        _brackets = brackets.OrderBy(b => b.MinAmount).ToList();


        // 2. Çakışma kontrolü
        for (int i = 1; i < _brackets.Count; i++)
        {

            if (_brackets[i].MinAmount <= _brackets[i - 1].MaxAmount)
            {
                throw new ArgumentException("Tax brackets cannot overlap.");
            }
                
        }
    }
}
