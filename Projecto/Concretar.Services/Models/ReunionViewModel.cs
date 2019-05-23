using Concretar.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Concretar.Services.Models
{
    public class ReunionViewModel
    {
        public int ReunionId { get; set; }
        [Required(ErrorMessage = "La fecha es requerida")]
        public DateTime Fecha { get; set; }
        [Required(ErrorMessage = "El motivo es requerido")]
        public string Motivo { get; set; }
        public string Resultado { get; set; }
        [Required(ErrorMessage = "El cliente es requerido")]
        public int ClienteId { get; set; }
        [Required(ErrorMessage = "El usuario es requerido")]
        public int UsuarioId { get; set; }
        public Cliente Cliente { get; set; }
        public Usuario Usuario { get; set; }
    }
}
