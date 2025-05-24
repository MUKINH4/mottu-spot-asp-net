using mottu_spot.Model;

namespace mottu_spot.DTO
{
    public class MotoDTO
    {
        public long? Id { get; set; }
        public string Placa { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
        public long? PatioId { get; set; }
    }
}