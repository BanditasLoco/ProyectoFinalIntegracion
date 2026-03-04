using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenGrupalIntegracion.Models
{
    [Table("dept_emp")]
    public class DeptEmp
    {
        [Key]
        [Column("emp_no")]
        public int EmpNo { get; set; }

        [Key]
        [Column("dept_no")]
        public int DeptNo { get; set; }

        [StringLength(50)]
        [Column("from_date")]
        public string? FromDate { get; set; }

        [StringLength(50)]
        [Column("to_date")]
        public string? ToDate { get; set; }

        // Relaciones de navegación
        [ForeignKey("EmpNo")]
        public virtual Employee Employee { get; set; } = null!;

        [ForeignKey("DeptNo")]
        public virtual Department Department { get; set; } = null!;
    }
}