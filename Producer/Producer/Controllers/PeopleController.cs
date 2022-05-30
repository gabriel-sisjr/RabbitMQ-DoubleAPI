using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService personService) => _peopleService = personService;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var peoples = await _peopleService.GetAllAsync();
            return Ok(peoples);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] People p)
        {
            if (await _peopleService.InsertAsync(p))
                return Ok();

            return BadRequest();
        }
    }
}