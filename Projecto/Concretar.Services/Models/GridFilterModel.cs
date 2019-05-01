using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Services.Models
{
    public class GridFilterModel
    {
        public int rowPerPages { get; set; }
        public string fechaDesde { get; set; } = null;
        public string fechaHasta { get; set; } = null;
        public int? page { get; set; } = null;
        public int? rows { get; set; } = null;
    }
}
