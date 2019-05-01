using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Concretar.Entities;

namespace Concretar.Services.Models
{
    public class PerfilModel
    {
        [Required(ErrorMessage = "Ingrese un nombre.")]
        public string NombreRol { get; set; }
        public int RolId { get; set; }
        public List<VistaPermiso> ListaVistaPermiso { get; set; }
        public class VistaPermiso
        {
            public Permiso Permiso { get; set; }
            public bool Activo { get; set; }
        }
        public bool IsPerfilAdmin { get; set; }
    }
}
