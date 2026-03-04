using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenGrupalIntegracion.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("emp_no")]
        public int EmpNo { get; set; }

        [Required]
        [StringLength(100)]
        [Column("usuario")]
        public string Usuario { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [Column("clave")]
        public string Clave { get; set; } = string.Empty;

        // Relación con Employee
        [ForeignKey("EmpNo")]
        public virtual Employee Employee { get; set; } = null!;
    }
}