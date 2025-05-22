namespace mottu_spot.DTO
{
    public class PatioDTO {
        public long Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }

        public string? Nome { get; set; }
        public EnderecoDTO Endereco { get; set; }
        public List<MotoResponseDTO> Motos { get; set; }
    };
}
