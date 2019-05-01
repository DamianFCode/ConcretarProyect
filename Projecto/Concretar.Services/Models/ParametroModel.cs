using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Concretar.Services.Models
{
    public class ParametroModel
    {   
        public int ParametroId { get; set; }
        [Required(ErrorMessage = "Ingrese una clave")]
        [MaxLength(50, ErrorMessage = "La clave no puede superar los {1} caracteres")]
        public string Clave { get; set; }
        [MaxLength(200, ErrorMessage = "La clave no puede superar los {1} caracteres")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "Ingrese un valor")]
        [MaxLength(50, ErrorMessage = "La valor no puede superar los {1} caracteres")]
        public string Valor { get; set; }
        [Required(ErrorMessage = "Ingrese una categoria")]
        [MaxLength(50, ErrorMessage = "La categoria no puede superar los {1} caracteres")]
        public string Categoria { get; set; }
    }
}
