using Newtonsoft.Json;
using Concretar.Entities.Repository.Interface;

namespace Concretar.Entities
{
    public class UsuarioRol : IEntity
    {
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
        public int RolId { get; set; }
        [JsonIgnore]
        public virtual Rol Rol { get; set; }
    }
}
