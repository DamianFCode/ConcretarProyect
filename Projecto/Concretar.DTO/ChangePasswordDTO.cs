using System.ComponentModel.DataAnnotations;

namespace Concretar.DTO
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "El campo Contraseña Actual es obligatorio.")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña Actual")]
        public string ActualPassword { get; set; }

        [Required(ErrorMessage = "El campo Nueva Contraseña es obligatorio.")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva Contraseña")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "La confirmación de Contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y su confirmación no coinciden.")]
        public string ConfirmNewPassword { get; set; }

        public string Token { get; set; }
        public  string Email { get; set; }
    }
}
