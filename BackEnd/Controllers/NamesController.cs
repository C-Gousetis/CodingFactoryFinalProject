using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RandomNameGenerator.Application.Services;

namespace RandomNameGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class NamesController : ControllerBase
    {
        
        private readonly IName _service;

        public NamesController(IName service)
        {
            _service = service;
        }

        [HttpGet(Name = "{genderId}")]
        public async Task<IActionResult> GetNames(bool genderId)
        {
            return Ok(await _service.GetNames(genderId));
        }


    }
}
