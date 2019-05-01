using System.ComponentModel.DataAnnotations;
using Concretar.Entities.Repository.Interface;

namespace Concretar.Entities
{

    public class Parametro : IEntity
    {
        [Key]
        public int ParametroId { get; set; }
        public string Clave { get; set; }
        public string Descripcion { get; set; }
        public string Valor { get; set; }
        public string Categoria { get; set; }
    }
}

