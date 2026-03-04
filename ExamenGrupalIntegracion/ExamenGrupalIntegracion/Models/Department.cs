using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenGrupalIntegracion.Models
{
    [Table("departments")]
    public class Department
    {
        [Key]
        [Column("dept_no")]
        public int DeptNo { get; set; }

        [Required]
        [StringLength(50)]
        [Column("dept_name")]
        public string DeptName { get; set; } = string.Empty;

        // Relaciones de navegación
        public virtual ICollection<DeptEmp> DeptEmps { get; set; } = new List<DeptEmp>();
        public virtual ICollection<DeptManager> DeptManagers { get; set; } = new List<DeptManager>();
    }
}