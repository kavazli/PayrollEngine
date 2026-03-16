using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollEngine.Domain.Interfaces.Services;
using System.Text;

namespace PayrollEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerContributionsController : ControllerBase
    {
        private readonly IEmployerContributionsService _employerContributionsService;

        public EmployerContributionsController(IEmployerContributionsService employerContributionsService)
        {
            _employerContributionsService = employerContributionsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _employerContributionsService.GetAsync();
            return Ok(result);
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var result = await _employerContributionsService.GetAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Ay;İşveren SGK Primi;İşveren İşsizlik Primi;Toplam İşveren Maliyeti");

            foreach (var r in result.OrderBy(x => x.Month))
            {
                sb.AppendLine(string.Join(";", new object[]
                {
                    (int)r.Month,
                    r.EmployerSSContributionAmount,
                    r.EmployerUIContributionAmount,
                    r.TotalEmployerCost
                }.Select(v => v?.ToString()?.Replace('.', ','))));
            }

            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
            return File(bytes, "text/csv", "employer_contributions.csv");
        }
    }
}
