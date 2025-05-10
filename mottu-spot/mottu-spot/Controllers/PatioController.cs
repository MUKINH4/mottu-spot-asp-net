using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mottu_spot.DTO;
using mottu_spot.Model;
using mottu_spot.Services;

namespace mottu_spot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatioController : Controller
    {
        private readonly PatioService _patioService;

        public PatioController(PatioService patioService)
        {
            this._patioService = patioService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarPatio([FromBody] PatioDTO patioDto)
        {
            if (patioDto == null)
            {
                return BadRequest();
            }
            var createdPatio = await _patioService.CriarPatioAsync(patioDto);
            return CreatedAtAction(nameof(BuscarPatioPorId), new { id = createdPatio.Id }, createdPatio );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patio>> BuscarPatioPorId(long id)
        {
            var patio = await _patioService.BuscarPatioPorIdAsync(id);

            if (patio == null)
            {
                return NotFound();
            }

            return Ok(patio);
        }
    }
}
