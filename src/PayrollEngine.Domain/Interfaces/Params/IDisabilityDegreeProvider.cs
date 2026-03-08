

using PayrollEngine.Domain.Entities;


namespace PayrollEngine.Domain.Interfaces.Params;


// Bu arayüz, belirli bir yıl için engellilik derecelerini sağlayan bir hizmeti temsil eder. 
//Engellilik dereceleri, çalışanların engellilik durumlarına göre farklı vergi avantajları veya kesintiler alabileceği durumlarda önemlidir. 
//Bu arayüz, uygulamanın farklı bölümlerinde engellilik derecelerine ihtiyaç duyulduğunda kullanılabilir.
public interface IDisabilityDegreeProvider
{
    public Task<List<DisabilityDegree>> GetValueAsync(int year);
    
}
