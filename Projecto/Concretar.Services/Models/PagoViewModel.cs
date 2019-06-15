using Concretar.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Services.Models
{
    public class PagoViewModel
    {
        public int PagoId { get; set; }
        public DateTime Fecha { get; set; }
        public string NumeroComprobante { get; set; }
        public int CuotaId { get; set; }
        public Cuota Cuota { get; set; }
    }
}
