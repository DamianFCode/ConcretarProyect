using Concretar.Entities.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Entities
{
    public class CommonEntity : IEntity
    {
        public bool Habilitado { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public string DeleteBy { get; set; }
        public DateTime? TSCreate { get; set; }
        public DateTime? TSModificado { get; set; }
        public DateTime? TSEliminado { get; set; }
    }
}
