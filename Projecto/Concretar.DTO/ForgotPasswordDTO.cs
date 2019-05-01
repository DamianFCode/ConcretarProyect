using System.ComponentModel.DataAnnotations;

namespace Concretar.DTO
{
    public class ForgotPasswordDTO
    {
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Formato de email inválido.")]
        public string Email { get; set; }
        public string UrlSuccessRedirect { get; set; }

    }
}
