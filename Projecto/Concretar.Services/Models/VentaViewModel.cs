using Concretar.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Services.Models
{
    public class VentaViewModel
    {
        public int VentaId { get; set; }
        public string Interes { get; set; }
        public int CantidadCuotas { get; set; }
        public DateTime Fecha { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int? ProyectoId { get; set; }
        public Proyecto Proyecto { get; set; }
        public int? LoteId { get; set; }
        public Lote Lote { get; set; }
        public int CuotaId { get; set; }
        public Cuota Cuota { get; set; }
    }
}
