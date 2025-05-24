using mottu_spot.Model;

namespace mottu_spot.DTO
{
    public class MotoResponseDTO
    {
        public long Id { get; set; }
        public string Placa { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public long? PatioId { get; set; }

        public Dispositivo Dispositivo { get; set; }
    }
}
