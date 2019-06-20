using Concretar.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Services.Models
{
    public class CuotaViewModel
    {
        public int CuotaId { get; set; }
        public string Precio { get; set; }
        public string TotalPagado { get; set; }
        public string SubTotal { get; set; }
        public bool Confirmado { get; set; }
        public string NumeroCuota { get; set; }
        public bool Mora { get; set; }
        public string MontoMora { get; set; }
        public int VentaId { get; set; }
        public Venta Venta { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
