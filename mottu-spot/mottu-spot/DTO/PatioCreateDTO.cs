namespace mottu_spot.DTO
{
    public class PatioCreateDTO
    {
        public long Id { get; set; }

        public string? Nome { get; set; }
        public EnderecoDTO Endereco { get; set; }
    }
}
