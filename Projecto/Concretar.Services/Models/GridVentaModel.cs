using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Services.Models
{
    public class GridVentaModel
    {
        public int TotalRows { get; set; }
        public List<VentaViewModel> ListVentas { get; set; }
    }
}
