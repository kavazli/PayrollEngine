using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollEngine.Domain.Interfaces.Services;
using System.Text;

namespace PayrollEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingVoucherController : ControllerBase
    {
        private readonly IShoppingVoucherService _shoppingVoucherService;

        public ShoppingVoucherController(IShoppingVoucherService shoppingVoucherService)
        {
            _shoppingVoucherService = shoppingVoucherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _shoppingVoucherService.GetAsync();
            return Ok(result);
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var result = await _shoppingVoucherService.GetAsync();

            var sb = new StringBuilder();
            sb.AppendLine("Ay;Brüt Tutar;Gelir Vergisi;Damga Vergisi;Net Tutar");

            foreach (var r in result.OrderBy(x => x.Month))
            {
                sb.AppendLine(string.Join(";", new object[]
                {
                    (int)r.Month,
                    r.GrossAmount,
                    r.IncomeTax,
                    r.StampTax,
                    r.NetAmount
                }.Select(v => v?.ToString()?.Replace('.', ','))));
            }

            var bytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
            return File(bytes, "text/csv", "shopping_voucher.csv");
        }
    }
}
