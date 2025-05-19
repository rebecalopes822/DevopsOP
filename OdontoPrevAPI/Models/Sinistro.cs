using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OdontoPrevAPI.Models
{
    [Table("SINISTRO")]
    public class Sinistro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_SINISTRO")]
        public int Id { get; set; }

        [Required]
        [Column("ID_PACIENTE")]
        public int PacienteId { get; set; }

        [JsonIgnore]
        public virtual Paciente? Paciente { get; set; }

        [Required]
        [Column("ID_TRATAMENTO")]
        public int TratamentoId { get; set; }

        [JsonIgnore]
        public virtual Tratamento? Tratamento { get; set; }

        [Required]
        [Column("DATA_OCORRENCIA")]
        public DateTime DataOcorrencia { get; set; }

        [Column("STATUS")]
        public string Status { get; set; }
    }
}
