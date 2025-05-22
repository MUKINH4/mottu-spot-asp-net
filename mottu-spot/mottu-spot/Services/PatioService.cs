using mottu_spot.Data;
using mottu_spot.DTO;
using mottu_spot.Model;
using Microsoft.EntityFrameworkCore;

namespace mottu_spot.Services
{
    public interface IPatioService
    {
        Task<Patio> CriarPatioAsync(PatioDTO patioDto);
        Task<List<Patio>> ListPatiosAsync();
        Task<Patio?> BuscarPatioPorIdAsync(long id);
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

        public async Task<Patio> CriarPatioAsync(PatioDTO patioDto)
        {
            var endereco = new Endereco
            {
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

        public async Task<List<Patio>> ListPatiosAsync()
        {
            return await _context.Patios
                .Include(p => p.Endereco)
                .Include(p => p.Motos)
                .ToListAsync();
        }

        public async Task<Patio?> BuscarPatioPorIdAsync(long id)
        {
            return await _context.Patios
                .Include(p => p.Endereco)
                .Include(p => p.Motos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task DeletarPatioAsync(long id)
        {
            var patio = await _context.Patios
                .Include(p => p.Endereco)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patio == null)
                throw new ArgumentException("Pátio não encontrado");

            if (patio.Endereco != null)
                _context.Enderecos.Remove(patio.Endereco);

            _context.Patios.Remove(patio);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarPatioAsync(long id, PatioDTO patioDto)
        {
            var patio = await _context.Patios
                .Include(p => p.Endereco)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patio == null)
                throw new ArgumentException("Pátio não encontrado");

            if (patioDto.Nome == null)
                throw new ArgumentException("nome: nome não pode ser nulo");

            // Atualiza endereço
            if (patio.Endereco == null)
            {
                patio.Endereco = new Endereco();
                _context.Enderecos.Add(patio.Endereco);
            }

            patio.Endereco.Cep = patioDto.Cep;
            patio.Endereco.Pais = patioDto.Pais;
            patio.Endereco.Cidade = patioDto.Cidade;
            patio.Endereco.Estado = patioDto.Estado;
            patio.Endereco.Logradouro = patioDto.Logradouro;
            patio.Endereco.Bairro = patioDto.Bairro;
            patio.Endereco.Numero = patioDto.Numero;

            patio.Nome = patioDto.Nome;

            await _context.SaveChangesAsync();
        }
    }
}