using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Services.Models
{
    public class GridReunionModel
    {
        public int TotalRows { get; set; }
        public List<ReunionViewModel> ListReuniones { get; set; }
    }
}
