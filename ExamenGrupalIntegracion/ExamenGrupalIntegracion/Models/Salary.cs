using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenGrupalIntegracion.Models
{
    [Table("salaries")]
    public class Salary
    {
        [Key]
        [Column("emp_no")]
        public int EmpNo { get; set; }

        [Column("salary")]
        public long SalaryAmount { get; set; }

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