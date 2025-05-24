using mottu_spot.Data;
using mottu_spot.DTO;
using mottu_spot.Model;
using Microsoft.EntityFrameworkCore;

namespace mottu_spot.Services
{
    public interface IPatioService
    {
        Task<Patio> CriarPatioAsync(PatioCreateDTO patioDto);
        Task<List<Patio>> ListPatiosAsync();
        Task<Patio?> BuscarPatioPorIdAsync(long id);
        Task DeletarPatioAsync(long id);
        Task AtualizarPatioAsync(long id, PatioCreateDTO patioDto);
    }

    public class PatioService : IPatioService
    {
        private readonly AppDbContext _context;

        public PatioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Patio> CriarPatioAsync(PatioCreateDTO patioDto)
        {
            var endereco = new Endereco
            {
                Cep = patioDto.Endereco.Cep,
                Pais = patioDto.Endereco.Pais,
                Cidade = patioDto.Endereco.Cidade,
                Estado = patioDto.Endereco.Estado,
                Logradouro = patioDto.Endereco.Logradouro,
                Bairro = patioDto.Endereco.Bairro,
                Numero = patioDto.Endereco.Numero
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

            try
            {
                if (patio.Endereco != null)
                    _context.Enderecos.Remove(patio.Endereco);

                _context.Patios.Remove(patio);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException != null && ex.InnerException.Message.Contains("ORA-02292"))
            {
                throw new InvalidOperationException("Não é possível apagar pátios com motos associadas.");
            }
        }

        public async Task AtualizarPatioAsync(long id, PatioCreateDTO patioDto)
        {
            var patio = await _context.Patios
                .Include(p => p.Endereco)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patio == null)
                throw new ArgumentException("Pátio não encontrado");

            if (patioDto.Nome == null)
                throw new ArgumentException("nome: nome não pode ser nulo");

            // Atualiza nome
            patio.Nome = patioDto.Nome;

            // Atualiza endereço
            if (patio.Endereco == null)
            {
                patio.Endereco = new Endereco();
                _context.Enderecos.Add(patio.Endereco);
            }

            if (patioDto.Endereco == null)
                throw new ArgumentException("Endereço não pode ser nulo");

            patio.Endereco.Cep = patioDto.Endereco.Cep;
            patio.Endereco.Pais = patioDto.Endereco.Pais;
            patio.Endereco.Cidade = patioDto.Endereco.Cidade;
            patio.Endereco.Estado = patioDto.Endereco.Estado;
            patio.Endereco.Logradouro = patioDto.Endereco.Logradouro;
            patio.Endereco.Bairro = patioDto.Endereco.Bairro;
            patio.Endereco.Numero = patioDto.Endereco.Numero;

            await _context.SaveChangesAsync();
        }
    }
}