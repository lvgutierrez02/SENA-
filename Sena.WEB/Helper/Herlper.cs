﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Sena.WEB.Helpers
{
    public class Helper
    {
        public static string RenderRazorViewToString(Controller controller, string viewName, object model = null) // carga el modal con los errores nuevamente si no ingresó los campos requeridos
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)] //Redirecciona a donde yo le diga si la ruta que ingresó de manera directa por el navegador no se encuentra, solo ingresa a traves de javascript
    public class NoDirectAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.GetTypedHeaders().Referer == null ||
                filterContext.HttpContext.Request.GetTypedHeaders().Host.Host.ToString() != filterContext.HttpContext.Request.GetTypedHeaders().Referer.Host.ToString())
            {
                filterContext.HttpContext.Response.Redirect("../Clientes/Index"); //Redirecciona a donde yo le diga si la ruta que ingresó de manera directa no se encuentra
                filterContext.HttpContext.Response.Redirect("../Clientes/Crear");
                filterContext.HttpContext.Response.Redirect("../Clientes/Detalle");
                filterContext.HttpContext.Response.Redirect("../Clientes/Editar");
            }
        }
    }

}