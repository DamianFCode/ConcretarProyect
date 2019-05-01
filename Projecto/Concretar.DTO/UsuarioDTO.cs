using System;
using System.ComponentModel.DataAnnotations;

namespace Concretar.DTO
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string Nombre { set; get; }
        [Required(ErrorMessage = "El campo apellido es obligatorio.")]
        public string Apellido { set; get; }
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Formato de email inválido.")]
        public string Email { set; get; }
        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        public string Usuario { set; get; }
        [Required(ErrorMessage = "El campo Contraseña es Obligatorio")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { set; get; }
        public string Token { set; get; }
        public DateTime? Expiration { set; get; }
    }
}
