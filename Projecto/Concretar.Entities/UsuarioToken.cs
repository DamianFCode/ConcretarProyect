using System;
using System.Collections.Generic;
using System.Text;
using Concretar.Entities.Repository.Interface;

namespace Concretar.Entities
{
    public class UsuarioToken : IEntity
    {
        public int UsuarioTokenId { set; get; }
        public string Token { set; get; }
        public int UsuarioId { set; get; }
        public bool Usado { set; get; }
        public DateTime FechaExpiracion { set; get; }
    }
}
