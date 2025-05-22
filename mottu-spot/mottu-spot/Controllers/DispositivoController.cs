using Microsoft.AspNetCore.Mvc;
using mottu_spot.DTO;
using mottu_spot.Model;
using mottu_spot.Services;

namespace mottu_spot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DispositivoController : ControllerBase
    {
        private readonly DispositivoService _dispositivoService;

        public DispositivoController(DispositivoService dispositivoService)
        {
            _dispositivoService = dispositivoService;
        }

        // POST: api/dispositivo
        [HttpPost]
        public async Task<ActionResult<Dispositivo>> CriarDispositivo([FromBody] DispositivoDTO dto)
        {
            if (dto == null || dto.MotoId == null)
                return BadRequest();

            var dispositivo = await _dispositivoService.CriarDispositivoAsync(dto.MotoId.Value);
            return CreatedAtAction(nameof(CriarDispositivo), new { id = dispositivo.Id }, dispositivo);
        }

        // PUT: api/dispositivo/alarme/{motoId}
        [HttpPut("alarme/{motoId:long}")]
        public async Task<IActionResult> MudarEstadoAlarme(long motoId)
        {
            var result = await _dispositivoService.MudarEstadoAlarmeAsync(motoId);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}