using System.Collections.Generic;
using Newtonsoft.Json;
using Concretar.Entities.Repository.Interface;

namespace Concretar.Entities
{
    public class Permiso : IEntity
    {
        public int PermisoId { get; set; }
        public int VistaId { set; get; }
        public string Funcionalidad { set; get; }
        public string Descripcion { set; get; }
        [JsonIgnore]
        public virtual Vista Vista { set; get; }
        [JsonIgnore]
        public virtual ICollection<RolPermiso> RolPermisos { get; } = new List<RolPermiso>();
    }
}
