using System;
using System.Collections.Generic;
using System.Text;
using Concretar.Entities.Repository.Interface;

namespace Concretar.Entities
{
    public class Lote : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Precio { get; set; }
        public string Medida { get; set; }
        public string Descripcion { get; set; }

    }
}
