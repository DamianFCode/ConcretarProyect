﻿using Concretar.Entities.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Entities
{
    public class Proyecto : IEntity
    {
        public int ProyectoId { get; set; }
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public string Dimension { get; set; }
        public string Precio { get; set; }
        public string Descripcion { get; set; }
        public DateTime TSCreado { set; get; }
        public DateTime? TSModificado { set; get; }
        public DateTime? TSEliminado { set; get; }
        [JsonIgnore]
        public virtual Venta Venta { get; set; }
    }
}
