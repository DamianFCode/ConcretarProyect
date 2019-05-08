using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using System.Text;

namespace Concretar.Services.Models
{
    public class ClienteViewModel
    {
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Apellido es obligatorio")]
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [Required(ErrorMessage = "El campo Correo es obligatorio")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "El campo Teléfono es obligatorio")]
        public int Telefono { get; set; }
        [Required(ErrorMessage = "El campo Calle es obligatorio")]
        public string Domicilio { get; set; }
        [Required(ErrorMessage = "El campo Número de calle es obligatorio")]
        public int NumeroDomicilio { get; set; }
        public string Observacion { get; set; }

    }
}
