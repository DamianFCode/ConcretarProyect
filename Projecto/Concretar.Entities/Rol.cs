using System.Collections.Generic;
using Newtonsoft.Json;
using Concretar.Entities.Repository.Interface;

namespace Concretar.Entities
{
    public class Rol : IEntity
    {
        public int RolId { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        [JsonIgnore]
        public virtual ICollection<UsuarioRol> UsuarioRoles { get; } = new List<UsuarioRol>();
        [JsonIgnore]
        public virtual ICollection<RolPermiso> RolPermisos { get; } = new List<RolPermiso>();
    }
}