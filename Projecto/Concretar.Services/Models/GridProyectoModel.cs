using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Services.Models
{
    public class GridProyectoModel
    {
        public int TotalRows { get; set; }
        public List<ProyectoViewModel> ListProyecto { get; set; }
    }
}
