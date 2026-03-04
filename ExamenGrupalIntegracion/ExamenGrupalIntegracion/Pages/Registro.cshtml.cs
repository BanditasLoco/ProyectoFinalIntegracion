using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ExamenGrupalIntegracion.Data;
using ExamenGrupalIntegracion.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamenGrupalIntegracion.Pages
{
    public class RegistroModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegistroModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "La identificación es requerida")]
        [Display(Name = "Identificación")]
        public string IdEmpleado { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder 50 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder 50 caracteres")]
        public string Apellido { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Correo { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        [DataType(DataType.Password)]
        public string Clave { get; set; } = string.Empty;

        [BindProperty]
        [Required(ErrorMessage = "Debe seleccionar un género")]
        public string Genero { get; set; } = string.Empty;

        // Propiedades para mostrar mensajes
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }

        public void OnGet()
        {
            // Inicialización si es necesaria
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Validar si el correo ya existe
                var existeCorreo = await _context.Employees
                    .AnyAsync(e => e.Correo == Correo);

                if (existeCorreo)
                {
                    ErrorMessage = "Ya existe un empleado registrado con ese correo";
                    return Page();
                }

                // Validar si la identificación ya existe
                var existeCI = await _context.Employees
                    .AnyAsync(e => e.Ci == IdEmpleado);

                if (existeCI)
                {
                    ErrorMessage = "Ya existe un empleado con esa identificación";
                    return Page();
                }

                // Crear el empleado
                var empleado = new Employee
                {
                    Ci = IdEmpleado,
                    FirstName = Nombre,
                    LastName = Apellido,
                    Correo = Correo,
                    BirthDate = FechaNacimiento,  // Ya no conviertas a string
                    Gender = Genero,
                    HireDate = DateTime.Now       // Ya no conviertas a string
                };

                _context.Employees.Add(empleado);
                await _context.SaveChangesAsync();

                // Crear el usuario asociado
                // En el método OnPostAsync, cambiar la creación del usuario:

                var usuario = new User
                {
                    EmpNo = empleado.EmpNo,
                    Usuario = IdEmpleado,  // Usar CI como usuario en lugar de correo
                    Clave = Clave          // Guardar contraseña en texto plano (solo para desarrollo)
                };

                _context.Users.Add(usuario);
                await _context.SaveChangesAsync();

                SuccessMessage = "Empleado registrado exitosamente. Redirigiendo al login...";

                // Redirigir a login después de 2 segundos
                return RedirectToPage("/Login");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al registrar el empleado: {ex.Message}";
                return Page();
            }
        }
    }
}