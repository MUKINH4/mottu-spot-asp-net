using mottu_spot.Data;
using mottu_spot.DTO;
using mottu_spot.Model;

namespace mottu_spot.Services
{
    public interface IPatioService
    {
        Task<Patio> CriarPatioAsync(PatioDTO patioDto);
        Task<List<Patio>> ListPatiosAsync();
        Task<Patio> BuscarPatioPorIdAsync(long id);
        Task DeletarPatioAsync(long id);
        Task AtualizarPatioAsync(long id, PatioDTO patioDto);
    }

public class PatioService : IPatioService
    {
        private readonly AppDbContext _context;

        public PatioService(AppDbContext context)
        {
            _context = context;
        }

        public Task AtualizarPatioAsync(long id, PatioDTO patioDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Patio> CriarPatioAsync(PatioDTO patioDto) {
            var endereco = new Endereco { 
                Cep = patioDto.Cep,
                Pais = patioDto.Pais,
                Cidade = patioDto.Cidade,
                Estado = patioDto.Estado,
                Logradouro = patioDto.Logradouro,
                Bairro = patioDto.Bairro,
                Numero = patioDto.Numero
                };
            _context.Enderecos.Add(endereco);

            await _context.SaveChangesAsync();
            var patio = new Patio
            {
                Nome = patioDto.Nome,
                Endereco = endereco
            };

            _context.Patios.Add(patio);
            await _context.SaveChangesAsync();

            return patio;
        }

        public Task DeletarPatioAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Patio> BuscarPatioPorIdAsync(long id)
        {
            return await _context.Patios.FindAsync(id);
        }

        public Task<List<Patio>> ListPatiosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
