using Concretar.Entities.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Entities
{
    public class Reunion : IEntity
    {
        public int ReunionId { get; set; }
        public DateTime Fecha { get; set; }
        public string Motivo { get; set; }
        public string Resultado { get; set; }
        public int ClienteId { get; set; }
        public int UsuarioId { get; set; }
        [JsonIgnore]
        public virtual Cliente Cliente { get; set; }
        [JsonIgnore]
        public virtual Usuario Usuario { get; set; }
    }
}
