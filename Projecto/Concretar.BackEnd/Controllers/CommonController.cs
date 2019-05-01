using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Concretar.Backend.Controllers
{
    [Authorize(Policy = "UserAccess")]
    public class CommonController : Controller
    {
        /// <summary>
        /// Método útil para setear un par de tempData comunmente usado para enviar mensajes a la vista luego de un redirect.
        /// </summary>
        /// <param name="msg">Mensaje</param>
        /// <param name="type">tipo, usar clases de bootstrap: info, success, primary, default, warning, danger</param>
        public void SetTempData(string msg, string type = "success", string style = "notificacion")
        {
            TempData["message"] = msg;
            TempData["type"] = type;
            TempData["style"] = style;
        }
    }
}