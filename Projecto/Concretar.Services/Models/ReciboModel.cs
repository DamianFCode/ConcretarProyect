using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Services.Models
{
    public class ReciboModel
    {
        public CuotaViewModel Cuota { get; set; }
        public DateTime? NextVencimiento { get; set; }
    }
}
