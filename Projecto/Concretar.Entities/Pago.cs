using Concretar.Entities.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Entities
{
    public class Pago: IEntity
    {
        public int PagoId { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime TSCreado { set; get; }
        public DateTime? TSModificado { set; get; }
        public DateTime? TSEliminado { set; get; }
        public string NumeroComprobante { get; set; }
        public int CuotaId { get; set; }
        [JsonIgnore]
        public virtual Cuota Cuota { get; set; }
    }
}
