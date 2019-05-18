using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Services.Models
{
    public class GridClienteModel
    {
        public int TotalRows { get; set; }
        public List<ClienteViewModel> ListClientes { get; set; }
    }
}
