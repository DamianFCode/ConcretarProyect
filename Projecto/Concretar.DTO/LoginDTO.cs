using System.ComponentModel.DataAnnotations;

namespace Concretar.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Ingrese un Email.")]
        public string Email { set; get; }
        [Required(ErrorMessage = "Ingrese una contraseña.")]
        public string Contrasena { set; get; }
        public bool Recordarme { set; get; }
    }
}
