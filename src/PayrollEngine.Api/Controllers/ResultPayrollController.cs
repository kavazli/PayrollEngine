using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollEngine.Domain.Interfaces.Services;
using System.Text;

namespace PayrollEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultPayrollController : ControllerBase
    {
        private readonly IResultPayrollService _service;

        public ResultPayrollController(IResultPayrollService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAsync();
            return Ok(result);
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var result = await _service.GetAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Ay;Çalışma Günü;SGK Günü;Mevcut Maaş;Fazla Mesai 50%;Fazla Mesai 100%;Prim;Toplam Maaş;Brüt Maaş;SGK Matrahı;Çalışan SGK Kesintisi;Çalışan İşsizlik Kesintisi;GV Matrahı;Kümülatif GV Matrahı;GV İstisnası;Gelir Vergisi;GV Oranı;Damga Vergisi İstisnası;Damga Vergisi;Net Maaş");

            foreach (var r in result.OrderBy(x => x.Month))
            {
                sb.AppendLine(string.Join(";", new object[]
                {
                    (int)r.Month,
                    r.WorkDay,
                    r.SSDay,
                    r.CurrentSalary,
                    r.Overtime50Amount,
                    r.Overtime100Amount,
                    r.BonusAmount,
                    r.TotalSalary,
                    r.GrossSalary,
                    r.SSContributionBase,
                    r.EmployeeSSContributionAmount,
                    r.EmployeeUIContributionAmount,
                    r.IncomeTaxBase,
                    r.CumulativeIncomeTaxBase,
                    r.IncomeTaxExemption,
                    r.IncomeTax,
                    r.IncomeTaxRate,
                    r.StampTaxExemption,
                    r.StampTax,
                    r.NetSalary
                }.Select(v => v?.ToString()?.Replace('.', ','))));
            }

            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
            return File(bytes, "text/csv", "result_payroll.csv");
        }
    }
}
