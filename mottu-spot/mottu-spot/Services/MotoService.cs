using mottu_spot.DTO;
using mottu_spot.Model;
using Microsoft.EntityFrameworkCore;
using mottu_spot.Data;
using mottu_spot.Model.Enums;

namespace mottu_spot.Services
{
    public class MotoService
    {
        private readonly AppDbContext _context;

        public MotoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Moto> AdicionarMotoAsync(MotoDTO motoDto)
        {
            var patio = await _context.Patios.FindAsync(motoDto.PatioId);
            if (patio == null)
                throw new Exception("Pátio não encontrado");

            var moto = new Moto
            {
                Placa = motoDto.Placa,
                Descricao = motoDto.Descricao,
                Status = Enum.Parse<StatusEnum>(motoDto.Status, true),
                Patio = patio
            };

            _context.Motos.Add(moto);
            await _context.SaveChangesAsync();
            return moto;
        }

        public async Task<Moto?> BuscarMotoPorIdAsync(long id)
        {
            return await _context.Motos
                .Include(m => m.Patio)
                .Include(m => m.Dispositivo)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Moto>> ListarMotosAsync()
        {
            return await _context.Motos
                .Include(m => m.Patio)
                .Include(m => m.Dispositivo)
                .ToListAsync();
        }

        public async Task DeletarMotoAsync(long id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto != null)
            {
                _context.Motos.Remove(moto);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Moto?> AtualizarMotoAsync(long id, MotoDTO motoDto)
        {
            var moto = await _context.Motos.Include(m => m.Patio).FirstOrDefaultAsync(m => m.Id == id);
            if (moto == null)
                return null;

            var patio = await _context.Patios.FindAsync(motoDto.PatioId);
            if (patio == null)
                throw new Exception("Pátio não encontrado");

            moto.Descricao = motoDto.Descricao;
            moto.Placa = motoDto.Placa;
            moto.Status = Enum.Parse<StatusEnum>(motoDto.Status, true);
            moto.Patio = patio;

            await _context.SaveChangesAsync();
            return moto;
        }
    }
}