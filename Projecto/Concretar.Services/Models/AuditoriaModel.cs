using System;

namespace Concretar.Services.Models
{
    public class AuditoriaModel
    {
        public string TransactionId { get; set; }
        public string UserName { get; set; }
        public DateTime Fecha { get; set; }
        public int AuditoriaId { get; set; }
        public string Entity { get; set; }
        public string EntityId { get; set; }
        public string Action { get; set; }
        public string Value { get; set; }
        public string Descripcion { get; set; }
    }
}
