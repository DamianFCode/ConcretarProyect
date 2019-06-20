using Concretar.Entities.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Entities
{
    public class Cuota : IEntity
    {
        public int CuotaId { get; set; }
        public string Precio { get; set; }
        public string TotalPagado { get; set; }
        public string SubTotal { get; set; }
        public bool Confirmado { get; set; }
        public int NumeroCuota { get; set; }
        public bool Mora { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string MontoMora { get; set; }
        public DateTime TSCreado { set; get; }
        public DateTime? TSModificado { set; get; }
        public DateTime? TSEliminado { set; get; }
        [JsonIgnore]
        public virtual Pago Pago { get; set; }
        public int VentaId { get; set; }
        [JsonIgnore]
        public virtual Venta Venta { get; set; }

    }
}
