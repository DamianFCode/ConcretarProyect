using System;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Concretar.Services.Models
{
    public class LoteViewModel
    {
        public int LoteId { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El campo Precio es obligatorio")]
        public string Precio { get; set; }
        [Required(ErrorMessage = "El campo Ubicación es obligatorio")]
        public string Ubicacion { get; set; }
        [Required(ErrorMessage = "El campo Dimensión es obligatorio")]
        public string Dimension { get; set; }
        public string Descripcion { get; set; }
        public int ProyectId { get; set; }

    }
}
