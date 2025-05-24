using Microsoft.AspNetCore.Mvc;
using mottu_spot.DTO;
using mottu_spot.Model;
using mottu_spot.Services;

namespace mottu_spot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MotoController : ControllerBase
    {
        private readonly MotoService _motoService;

        public MotoController(MotoService motoService)
        {
            _motoService = motoService;
        }

        private MotoResponseDTO ToResponseDTO(Moto moto)
        {
            Dispositivo? dispositivo = null;
            if (moto.Dispositivo != null)
            {
                dispositivo = new Dispositivo
                {
                    Id = moto.Dispositivo.Id,
                    Ativo = moto.Dispositivo.Ativo
                };
            }
            return new MotoResponseDTO
            {
                Id = moto.Id,
                Placa = moto.Placa,
                Descricao = moto.Descricao,
                Status = moto.Status.ToString(),
                PatioId = moto.Patio?.Id,
                Dispositivo = dispositivo
            };
        }

        // POST: api/moto
        [HttpPost]
        public async Task<ActionResult<Moto>> AdicionarMoto([FromBody] MotoDTO motoDto)
        {
            if (motoDto == null)
                return BadRequest();

            var moto = await _motoService.AdicionarMotoAsync(motoDto);
            return CreatedAtAction(nameof(BuscarMotoPorId), new { id = moto.Id }, moto);
        }

        // GET: api/moto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MotoDTO>>> ListarMotos()
        {
            var motos = await _motoService.ListarMotosAsync();
            var result = motos.Select(ToResponseDTO).ToList();
            return Ok(result);
        }

        // GET: api/moto/{id}
        [HttpGet("{id:long}")]
        public async Task<ActionResult<MotoDTO>> BuscarMotoPorId(long id)
        {
            var moto = await _motoService.BuscarMotoPorIdAsync(id);
            if (moto == null)
                return NotFound();

            return Ok(ToResponseDTO(moto));
        }

        // DELETE: api/moto/{id}
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeletarMoto(long id)
        {
            var moto = await _motoService.BuscarMotoPorIdAsync(id);
            if (moto == null)
                return NotFound();

            await _motoService.DeletarMotoAsync(id);
            return NoContent();
        }

        // PUT: api/moto/{id}
        [HttpPut("{id:long}")]
        public async Task<ActionResult<Moto>> AtualizarMoto(long id, [FromBody] MotoDTO motoDto)
        {
            var moto = await _motoService.BuscarMotoPorIdAsync(id);
            if (moto == null)
                return NotFound();

            var updated = await _motoService.AtualizarMotoAsync(id, motoDto);
            return Ok(updated);
        }
    }
}