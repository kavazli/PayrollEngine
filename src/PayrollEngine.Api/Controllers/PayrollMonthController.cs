using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Services;

namespace PayrollEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollMonthController : ControllerBase
    {
        private readonly IPayrollMonthService _service;
        private readonly IEmployeeScenariosService _scenario;

        public PayrollMonthController(IPayrollMonthService service, IEmployeeScenariosService scenario)
        {
            _service = service;
            _scenario = scenario;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAsync();
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] List<PayrollTemplateMonth> payrolss) 
        {   
            
            var scenario = await _scenario.GetAsync();
            var result = await _service.AddAsync(payrolss, scenario);
            return Ok(result);
        }
        

        [HttpPut]
        public async Task<IActionResult> Set([FromBody] List<PayrollMonth> payrolss)
        {
            await _service.SetAsync(payrolss);
            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> Clear()
        {           
            await _service.ClearAsync();
            return NoContent();     
        }
    }
}
