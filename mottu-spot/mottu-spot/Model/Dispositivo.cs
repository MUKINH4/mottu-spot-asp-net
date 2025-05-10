using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mottu_spot.Model
{
    public class Dispositivo
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public bool Ativo {  get; set; }
        [ForeignKey("MotoId")]
        public long MotoId;
        public virtual Moto? Moto { get; set; }
        public Dispositivo()
        { 
            Id = Guid.NewGuid();
            Ativo = false;
        }

        public Dispositivo(Guid id, bool ativo)
        {
            Id = id;
            Ativo = ativo;
        }
    }
}
