namespace mottu_spot.DTO
{
    public class PatioDTO {
        public long Id { get; set; }

        public string? Nome { get; set; }
        public EnderecoDTO Endereco { get; set; }
        public List<MotoResponseDTO> Motos { get; set; }
    };
}
