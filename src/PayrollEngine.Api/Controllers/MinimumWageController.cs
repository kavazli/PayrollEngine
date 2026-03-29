using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollEngine.Application.Services.Params;
using PayrollEngine.Domain.Interfaces;

namespace PayrollEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinimumWageController : ControllerBase
    {

        private readonly MinimumWageService _minimumWageService;

        public MinimumWageController(MinimumWageService minimumWageService)
        {
            _minimumWageService = minimumWageService;
        }

        [HttpGet("{year}")]
        public async Task<IActionResult> GetMinimumWage(int year)
        {   
            var result = await _minimumWageService.GetValueAsync(year);
            return Ok(result);
        }


    }
}
