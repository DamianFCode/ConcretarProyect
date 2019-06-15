using System;
using System.Collections.Generic;
using System.Text;
using Concretar.Entities.Repository.Interface;
using Newtonsoft.Json;

namespace Concretar.Entities
{
    public class Lote : IEntity
    {
        public int LoteId { get; set; }
        public string Nombre { get; set; }
        public string Precio { get; set; }
        public string Ubicacion { get; set; }
        public string Dimension { get; set; }
        public string Descripcion { get; set; }
        public int ProyectId { get; set; }
        public DateTime TSCreado { set; get; }
        public DateTime? TSModificado { set; get; }
        public DateTime? TSEliminado { set; get; }
        [JsonIgnore]
        public virtual Venta Venta { get; set; }
    }
}
