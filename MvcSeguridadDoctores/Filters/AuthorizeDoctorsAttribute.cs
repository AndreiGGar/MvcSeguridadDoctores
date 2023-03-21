using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace MvcSeguridadDoctores.Filters
{
    public class AuthorizeDoctorsAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            string controller = context.RouteData.Values["controller"].ToString();
            string action = context.RouteData.Values["action"].ToString();
            string idenfermo = "";
            if (context.RouteData.Values.ContainsKey("id"))
            {
                idenfermo = context.RouteData.Values["id"].ToString();
            }
            /*RepositoryEmpleados repo = context.HttpContext.RequestServices.GetService<RepositoryEmpleados>();*/
            ITempDataProvider provider = context.HttpContext.RequestServices.GetService<ITempDataProvider>();
            var TempData = provider.LoadTempData(context.HttpContext);
            TempData["controller"] = controller;
            TempData["action"] = action;
            TempData["id"] = idenfermo;
            provider.SaveTempData(context.HttpContext, TempData);

            if (user.Identity.IsAuthenticated == false)
            {
                context.Result = this.GetRoute("Managed", "Login");
            }
            else
            {
                /*context.Result = this.GetRoute("Managed", "ErrorAcceso");*/
                /*if (user.IsInRole("PRESIDENTE") == false && user.IsInRole("DIRECTOR") == false && user.IsInRole("ANALISTA") == false)
                {
                    context.Result = this.GetRoute("Managed", "ErrorAcceso");
                }*/
            }
        }

        private RedirectToRouteResult GetRoute(string controller, string action)
        {
            RouteValueDictionary ruta = new RouteValueDictionary(new
            {
                controller = controller,
                action = action
            });
            RedirectToRouteResult result = new RedirectToRouteResult(ruta);
            return result;
        }
    }
}
