using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Concretar.Services.Models
{
    public class UsuarioViewModel
    {
        public int UsuarioId { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo apellido es obligatorio.")]
        public string Apellido { get; set; }
        [Required(ErrorMessage = "El campo email es obligatorio.")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Formato de email inválido.")]
        [Remote("EmailExists", "Usuario", AdditionalFields = "UsuarioId", HttpMethod = "POST", ErrorMessage = "Ya hay un usuario registrado con este E-Mail.")]
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Contrasena { set; get; }
        public string PathImagenPerfil { set; get; }
        public string ArrayRoles { get; set; }

    }
}
