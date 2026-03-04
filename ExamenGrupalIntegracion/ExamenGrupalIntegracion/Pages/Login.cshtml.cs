using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using ExamenGrupalIntegracion.Data;
using Microsoft.EntityFrameworkCore;

namespace ExamenGrupalIntegracion.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "El usuario es requerido")]
        [Display(Name = "Usuario")]
        public string Email { get; set; } = string.Empty; // Mantiene el nombre pero ahora es usuario

        [BindProperty]
        [Required(ErrorMessage = "La contraseña es requerida")]
        [DataType(DataType.Password)]
        public string Clave { get; set; } = string.Empty;

        public string? ErrorMessage { get; set; }

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
                // Buscar usuario en la base de datos bdNomina
                var usuario = await _context.Users
                    .Include(u => u.Employee)
                    .FirstOrDefaultAsync(u => u.Usuario == Email && u.Clave == Clave);

                if (usuario == null)
                {
                    ErrorMessage = "Usuario o contraseña incorrectos";
                    return Page();
                }

                // Login exitoso
                TempData["SuccessMessage"] = $"Bienvenido {usuario.Employee.FirstName} {usuario.Employee.LastName}";
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al iniciar sesión: {ex.Message}";
                return Page();
            }
        }
    }
}