using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenGrupalIntegracion.Models
{
    [Table("titles")]
    public class Title
    {
        [Key]
        [Column("emp_no")]
        public int EmpNo { get; set; }

        [StringLength(50)]
        [Column("title")]
        public string TitleName { get; set; } = string.Empty;

        [StringLength(50)]
        [Column("from_date")]
        public string? FromDate { get; set; }

        [StringLength(50)]
        [Column("to_date")]
        public string? ToDate { get; set; }

        // Relación de navegación
        [ForeignKey("EmpNo")]
        public virtual Employee Employee { get; set; } = null!;
    }
}