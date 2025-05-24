using Microsoft.AspNetCore.Mvc;
using mottu_spot.DTO;
using mottu_spot.Model;
using mottu_spot.Services;

namespace mottu_spot.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatioController : ControllerBase
    {
        private readonly PatioService _patioService;

        public PatioController(PatioService patioService)
        {
            _patioService = patioService;
        }

        // POST: api/patio
        [HttpPost]
        public async Task<ActionResult<Patio>> AdicionarPatio([FromBody] PatioCreateDTO patioCreateDto)
        {
            if (patioCreateDto == null)
                return BadRequest();

            var createdPatio = await _patioService.CriarPatioAsync(patioCreateDto);
            return CreatedAtAction(nameof(BuscarPatioPorId), new { id = createdPatio.Id }, createdPatio);
        }

        // GET: api/patio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatioDTO>>> ListarPatios()
        {
            var patios = await _patioService.ListPatiosAsync();
            var result = patios.Select(p => new PatioDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Endereco = new EnderecoDTO
                {
                    Id = p.Endereco.Id,
                    Cep = p.Endereco.Cep,
                    Logradouro = p.Endereco.Logradouro,
                    Numero = p.Endereco.Numero,
                    Bairro = p.Endereco.Bairro,
                    Cidade = p.Endereco.Cidade,
                    Estado = p.Endereco.Estado,
                    Pais = p.Endereco.Pais
                },
                Motos = p.Motos?.Select(m => new MotoResponseDTO
                {
                    Id = m.Id,
                    Placa = m.Placa,
                    Descricao = m.Descricao,
                    Status = m.Status.ToString(),
                    PatioId = m.PatioId,
                    Dispositivo = m.Dispositivo
                }).ToList() ?? new List<MotoResponseDTO>()
            }).ToList();

            return Ok(result);
        }

        // GET: api/patio/{id}
        [HttpGet("{id:long}")]
        public async Task<ActionResult<Patio>> BuscarPatioPorId(long id)
        {
            var patio = await _patioService.BuscarPatioPorIdAsync(id);
            if (patio == null)
                return NotFound();

            return Ok(patio);
        }

        // DELETE: api/patio/{id}
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeletarPatio(long id)
        {
            try
            {
                var patio = await _patioService.BuscarPatioPorIdAsync(id);
                if (patio == null)
                    return NotFound();

                await _patioService.DeletarPatioAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/patio/{id}
        [HttpPut("{id:long}")]
        public async Task<ActionResult<Patio>> AtualizarPatio(long id, [FromBody] PatioCreateDTO patioDto)
        {
            var patio = await _patioService.BuscarPatioPorIdAsync(id);
            if (patio == null)
                return NotFound();

            await _patioService.AtualizarPatioAsync(id, patioDto);
            // Retorne o objeto atualizado, se desejar
            var updated = await _patioService.BuscarPatioPorIdAsync(id);
            return Ok(updated);
        }
    }
}