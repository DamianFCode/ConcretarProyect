using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Concretar.Services.Models
{
    public class ProyectoViewModel
    {
        public int ProyectoId { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Ubicacion es obligatorio")]
        public string Ubicacion { get; set; }
        [Required(ErrorMessage = "El campo Dimencion es obligatorio")]
        public string Dimencion { get; set; }
        [Required(ErrorMessage = "El campo Precio es obligatorio")]
        public string Precio { get; set; }
        public string Descripcion { get; set; }

    }
}
