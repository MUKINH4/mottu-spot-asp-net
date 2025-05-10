using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mottu_spot.Model
{
    [Table("Endereco")]
    public class Endereco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Cep { get; set; }

        [Required(ErrorMessage = "Logradouro não pode ser nulo")]
        public string Logradouro { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Número deve ser positivo e maior que 0")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Bairro não pode ser nulo")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Cidade não pode ser nulo")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Estado não pode ser nulo")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "País não pode ser nulo")]
        public string Pais { get; set; }

        public Endereco() { }

        public Endereco(string cep, string pais, string cidade, string estado, string logradouro, string bairro, int numero)
        {
            Cep = cep;
            Pais = pais;
            Cidade = cidade;
            Estado = estado;
            Logradouro = logradouro;
            Bairro = bairro;
            Numero = numero;
        }
    }
}
