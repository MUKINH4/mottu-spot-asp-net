using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mottu_spot.Model
{
    public class Patio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string? Nome { get; set; }

        [ForeignKey("EnderecoId")]
        public long EnderecoId;
        public virtual Endereco Endereco { get; set; }

        public DateTime DataAdicao { get; set; } = DateTime.Now;

        public List<Moto> Motos { get; set; }

        public Patio() { }

        public Patio(long id, string nome, long enderecoId, Endereco endereco, DateTime dataAdicao, List<Moto> motos)
        {
            Id = id;
            Nome = nome;
            EnderecoId = enderecoId;
            Endereco = endereco;
            DataAdicao = dataAdicao;
            Motos = motos;
        }
    }
}
