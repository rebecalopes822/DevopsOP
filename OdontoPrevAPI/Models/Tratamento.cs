using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OdontoPrevAPI.Models
{
    [Table("TRATAMENTO")]
    public class Tratamento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_TRATAMENTO")]
        public int Id { get; set; }

        [Required]
        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        [Required]
        [Column("TIPO")]
        public string Tipo { get; set; }

        [Required]
        [Column("CUSTO")]
        public decimal Custo { get; set; }
    }
}
