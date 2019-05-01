using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Concretar.Services;

namespace Concretar.Backend.Extensions
{
    public static class HtmlHelpers
    {
        public static String IsActive(this IHtmlHelper html, string controller = null, string action = null, bool IsGroup = false)
        {
            string activeClass = "navigation__active"; // change here if you another name to activate sidebar items
            // detect current app state
            string actualAction = (string)html.ViewContext.RouteData.Values["action"];
            string actualController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = actualController;

            if (String.IsNullOrEmpty(action))
                action = actualAction;

            var classGroup = IsGroup ? " navigation__sub--toggled" : String.Empty;

            return (controller == actualController && action == actualAction) ? activeClass + classGroup : String.Empty;
        }

        public static String IsActiveGroup(this IHtmlHelper html, string controller = null, string action = null)
        {
            string activeClass = "display: "; // change here if you another name to activate sidebar items
            // detect current app state
            string actualAction = (string)html.ViewContext.RouteData.Values["action"];
            string actualController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = actualController;

            if (String.IsNullOrEmpty(action))
                action = actualAction;

            return (controller == actualController && action == actualAction) ? activeClass + "block" : String.Empty;
        }

        public static IHtmlContent ActionLinkRole(this IHtmlHelper helper, string textMenu, string strClasses, string actionName, string controllerName, string Email, object UrlValues = null)
        {
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);
            if (VerifyAccess(helper, Email, actionName, controllerName))
            {

                return new HtmlString("<a href='" + urlHelper.Action(actionName, controllerName, UrlValues) + "' class='" + strClasses + "'>" + textMenu + "</a>");
            }
            else
            {
                return HtmlString.Empty;
            }
        }
        public static IHtmlContent ActionMenuRole(this IHtmlHelper helper, string textMenu, string iconClass, string actionName, string controllerName, string Email, object UrlValues = null)
        {
            var urlHelperFactory = (IUrlHelperFactory)helper.ViewContext.HttpContext.RequestServices.GetService(typeof(IUrlHelperFactory));
            var urlHelper = urlHelperFactory.GetUrlHelper(helper.ViewContext);
            if (VerifyAccess(helper, Email, actionName, controllerName))
            {

                return new HtmlString("<a href='" + urlHelper.Action(actionName, controllerName, UrlValues) + "'><i class='" + iconClass + "'></i>&nbsp;" + textMenu + "</a>");
            }
            else
            {
                return HtmlString.Empty;
            }
        }
        public static string ColumnTablePermission(this IHtmlHelper html, string Action, string Controller, string Email)
        {
            AuthenticationService au = new AuthenticationService();
            if (au.VerifyAccess(Email, Action, Controller))
            {
                return String.Empty;
            }
            else
            {
                return "table-col-hidden";
            }
        }

        public static string ItemLinkPermission(this IHtmlHelper html, string Action, string Controller, string Email)
        {
            AuthenticationService au = new AuthenticationService();
            if (au.VerifyAccess(Email, Action, Controller))
            {
                return String.Empty;
            }
            else
            {
                return "style='display:none;'";
            }
        }

        public static bool UserExists(this IHtmlHelper html, string Email)
        {
            AuthenticationService au = new AuthenticationService();
            return au.UserExists(Email);
        }
        public static bool VerifyAccess(this IHtmlHelper html, string Email, string Action, string Controller)
        {
            AuthenticationService au = new AuthenticationService();
            return au.VerifyAccess(Email, Action, Controller);
        }
    }
}
