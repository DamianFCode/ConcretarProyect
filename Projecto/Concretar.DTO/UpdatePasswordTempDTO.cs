using System.ComponentModel.DataAnnotations;

namespace Concretar.DTO
{
    public class UpdatePasswordTempDTO
    {
        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmación de Contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y su confirmación no coinciden.")]
        public string ConfirmPassword { get; set; }

        public string UserName { get; set; }
    }
}
