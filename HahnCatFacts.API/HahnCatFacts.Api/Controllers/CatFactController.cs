using HahnCatFacts.Application.DTOs;
using HahnCatFacts.Application.Interfaces.Services;
using HahnCatFacts.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HahnCatFacts.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatFactController : ControllerBase
    {
        private readonly ICatFactService _catFactService;

        public CatFactController(ICatFactService catFactService)
        {
            _catFactService = catFactService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CatFact>> GetAllCatFacts()
        {
            try
            {
                var catFacts = _catFactService.GetAllCatFacts(); 
                return Ok(catFacts);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch
            {
                return StatusCode(500, $"An unexpected error occurred.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCatFactAsync([FromBody] CatFactDto newCatFact)
        {
            if (newCatFact == null)
            {
                return BadRequest("No cat fact provided.");
            }

            try
            {
                await _catFactService.AddCatFactAsync(newCatFact);
                return Ok("Cat fact added successfully.");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ApplicationException ex) 
            { 
                return StatusCode(500, ex.Message); 
            }
            catch
            {
                return StatusCode(500, $"An unexpected error occurred.");
            }
        }
    }
}
