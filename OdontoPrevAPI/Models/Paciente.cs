using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OdontoPrevAPI.Models
{
    [Table("PACIENTE")]
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_PACIENTE")]
        public int Id { get; set; }

        [Required]
        [Column("NOME")]
        public string Nome { get; set; }

        [Required]
        [Column("EMAIL")]
        public string Email { get; set; }

        [Required]
        [Column("DATA_NASCIMENTO")]
        public DateTime DataNascimento { get; set; }

        [Required]
        [Column("TELEFONE")]
        public string Telefone { get; set; }

        [Column("ID_GENERO")]
        public int? GeneroId { get; set; }

        [Column("ID_ENDERECO")]
        public int? EnderecoId { get; set; }


        [JsonIgnore]
        public virtual ICollection<Sinistro> Sinistros { get; set; } = new List<Sinistro>();
    }
}
