using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamenGrupalIntegracion.Models
{
    [Table("employees")]
    public class Employee
    {
        [Key]
        [Column("emp_no")]
        public int EmpNo { get; set; }

        [Required]
        [StringLength(50)]
        [Column("ci")]
        public string Ci { get; set; } = string.Empty;

        [Required]
        [Column("birth_date")]
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [StringLength(1)]
        [Column("gender")]
        public string Gender { get; set; } = string.Empty;

        [Required]
        [Column("hire_date")]
        public DateTime HireDate { get; set; }

        [StringLength(100)]
        [Column("correo")]
        public string? Correo { get; set; }

        // Relaciones de navegación
        public virtual ICollection<DeptEmp> DeptEmps { get; set; } = new List<DeptEmp>();
        public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();
        public virtual ICollection<Title> Titles { get; set; } = new List<Title>();
        public virtual User? User { get; set; }
    }
}