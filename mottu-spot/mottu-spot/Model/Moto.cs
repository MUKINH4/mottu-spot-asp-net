using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using mottu_spot.Model.Enums;

namespace mottu_spot.Model
{
    public class Moto
    {
        public Moto() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required(ErrorMessage = "A placa é obrigatória")]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "A placa deve ter entre 6 e 10 caracteres")]
        [RegularExpression("^[A-Z0-9\\- ]{6,10}$", ErrorMessage = "Placa fora do padrão")]
        public string Placa { get; set; }

        [StringLength(500, ErrorMessage = "O máximo de caracteres é 500")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Status não pode ser nulo")]
        public StatusEnum Status { get; set; }

        public DateTime DataAdicao { get; set; } = DateTime.Now;

        [ForeignKey("PatioId")]
        public long? PatioId { get; set; }

        [JsonIgnore]
        public virtual Patio Patio { get; set; }

        public virtual Dispositivo Dispositivo { get; set; }
    }


}
