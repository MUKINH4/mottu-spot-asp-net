using mottu_spot.Model;
using mottu_spot.Data;
using Microsoft.EntityFrameworkCore;

namespace mottu_spot.Services
{
    public class DispositivoService
    {
        private readonly AppDbContext _context;
        private readonly MotoService _motoService;

        public DispositivoService(AppDbContext context, MotoService motoService)
        {
            _context = context;
            _motoService = motoService;
        }

        public async Task<bool> MudarEstadoAlarmeAsync(long motoId)
        {
            var dispositivo = await _context.Dispositivos.FirstOrDefaultAsync(d => d.MotoId == motoId);
            if (dispositivo == null)
                return false;

            dispositivo.Ativo = !dispositivo.Ativo;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Dispositivo> CriarDispositivoAsync(long motoId)
        {
            var moto = await _motoService.BuscarMotoPorIdAsync(motoId);
            if (moto == null)
                throw new Exception("Moto não encontrada");

            var dispositivo = new Dispositivo
            {
                Id = Guid.NewGuid(),
                Ativo = false,
                Moto = moto,
                MotoId = moto.Id
            };

            _context.Dispositivos.Add(dispositivo);
            await _context.SaveChangesAsync();
            return dispositivo;
        }
    }
}