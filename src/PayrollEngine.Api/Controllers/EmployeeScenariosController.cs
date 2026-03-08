using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PayrollEngine.Domain.Entities;
using PayrollEngine.Domain.Interfaces.Services;

namespace PayrollEngine.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeScenariosController : ControllerBase
    {
        
        private readonly IEmployeeScenariosService _service; 

        public EmployeeScenariosController(IEmployeeScenariosService service)
        {
            _service = service;
        }  

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] EmployeeScenario scenario)
        {
            var result = await _service.AddAsync(scenario);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Set([FromBody] EmployeeScenario scenario)
        {
            await _service.SetAsync(scenario);
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
