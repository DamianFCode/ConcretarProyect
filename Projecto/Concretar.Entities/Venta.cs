using Concretar.Entities.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Entities
{
    public class Venta : IEntity
    {
        public int VentaId { get; set; }
        public string Interes { get; set; }
        public int CantidadCuotas { get; set; }
        public string Anticipo { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime TSCreado { set; get; }
        public DateTime? TSModificado { set; get; }
        public DateTime? TSEliminado { set; get; }
        public int ClienteId { get; set; }
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }
        public int? ProyectoId { get; set; }
        [JsonIgnore]
        public virtual Proyecto Proyecto { get; set; }
        public int? LoteId { get; set; }
        [JsonIgnore]
        public virtual Lote Lote { get; set; }
        public int CuotaId { get; set; }
        [JsonIgnore]
        public virtual Cuota Cuota { get; set; }
    }
}
