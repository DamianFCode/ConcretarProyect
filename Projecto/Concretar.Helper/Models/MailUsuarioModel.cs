using System;
using System.Collections.Generic;
using System.Text;

namespace Concretar.Helper.Models
{
    public class MailUsuarioModel
    {
        public string Password { get; set; }
        public string Usuario { get; set; }
        public int UsuarioId { get; set; }
        public string TokenUrl { get; set; }
        public string UsuarioNombre { get; set; }
        public string UsuarioApellido { get; set; }
        public string Email { get; set; }
        public bool EsAdministrador { get; set; }
    }
}
