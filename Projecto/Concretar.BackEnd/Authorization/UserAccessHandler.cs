using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Controllers;
using Newtonsoft.Json;
using Concretar.Services;

namespace Concretar.BackEnd.Authorization
{
    public class UserAccessHandler : AuthorizationHandler<UserAccessAuthorization>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAccessAuthorization requirement)
        {
            var mvcContext = context.Resource as AuthorizationFilterContext;
            if (context.User.Claims.ToList().Count != 0)
            {
                AuthenticationService au = new AuthenticationService();
                var userEmail = context.User.Claims.FirstOrDefault(x => x.Type == "Usuario").Value;
                var user = au.GetUsuario(userEmail);
                if (user != null)
                {
                    if (mvcContext?.ActionDescriptor is ControllerActionDescriptor descriptor)
                    {
                        var actionName = descriptor.ActionName;
                        var ctrlName = descriptor.ControllerName;
                        var permiso = au.GetPermiso(actionName, ctrlName);
                        if (permiso != null && !au.HavePermission(user.UsuarioRoles.Select(x => x.RolId).ToList(), permiso.PermisoId))
                        {
                            mvcContext.Result = new ViewResult() { ViewName = "NoAutorizado" };
                        }
                    }
                    else
                    {
                        mvcContext.Result = new ViewResult() { ViewName = "NoAutorizado" };
                    }
                }
                else
                {
                    mvcContext.Result = new ViewResult() { ViewName = "NoAutorizado" };
                }
            }
            else
            {
                mvcContext.Result = new ViewResult() { ViewName = "NoAutorizado" };
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}
