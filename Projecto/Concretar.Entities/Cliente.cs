using System;
using System.Collections.Generic;
using System.Text;
using Concretar.Entities.Repository.Interface;

namespace Concretar.Entities
{
    public class Cliente : IEntity
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Edad { get; set; }
        public string DNI { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Domicilio { get; set; }
        public string NumeroDomicilio { get; set; }
        public string Observacion { get; set; }
    }
}
