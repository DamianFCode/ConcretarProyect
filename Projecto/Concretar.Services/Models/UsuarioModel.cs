using System;

namespace Concretar.Services.Models
{
    public class UsuarioModel
    {
        public int UsuarioId { set; get; }
        public string Apellido { set; get; }
        public string Nombre { set; get; }
        public string NombreUsuario { get; set; }
        public bool Habilitado { set; get; }
        public DateTime TSCreado { set; get; }
        public DateTime? TSModificado { set; get; }
        public DateTime? TSEliminado { set; get; }
        public string Contrasena { set; get; }
        public string Email { set; get; }
    }
}
