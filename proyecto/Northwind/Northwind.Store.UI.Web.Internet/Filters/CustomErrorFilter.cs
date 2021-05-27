using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Northwind.Store.UI.Web.Internet.Controllers;
using Northwind.Store.UI.Web.Internet.Models.Error;

namespace Northwind.Store.UI.Web.Internet.Filters
{
    public class CustomErrorFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || HasRecursedTooManyTimes(filterContext))
            {
                return;
            }

            var recursiveErrorMessage = GetRecursiveErrorMessage(filterContext.Exception);

            // if the request is AJAX return JSON else view.
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = new
                    {
                        error = true,
                        message = recursiveErrorMessage
                    }
                };

                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = filterContext.Exception is HttpException ? ((HttpException)filterContext.Exception).GetHttpCode() : 500;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

                return;
            }

            if (!filterContext.HttpContext.IsCustomErrorEnabled)
            {
                return;
            }

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            var model = new CustomHandleErrorInfo(filterContext.Exception, controllerName, actionName)
            {
                DisplayErrorMessage = recursiveErrorMessage
            };

            var controller = DependencyResolver.Current.GetService<ErrorController>();
            var routeData = new RouteData();
            var action = "Index";

            if (filterContext.Exception is HttpException)
            {
                var httpEx = filterContext.Exception as HttpException;

                switch (httpEx.GetHttpCode())
                {
                    case 404:
                        action = "NotFound";
                        break;

                        // others if any
                }
            }

            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = filterContext.Exception is HttpException ? ((HttpException)filterContext.Exception).GetHttpCode() : 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            routeData.Values["controller"] = "Error";
            routeData.Values["action"] = action;

            controller.ViewData.Model = model;
            ((IController)controller).Execute(new RequestContext(filterContext.HttpContext, routeData));
        }

        private string GetRecursiveErrorMessage(Exception ex, string delimeter = " --- ")
        {
            var sb = new StringBuilder();
            var currentException = ex;
            while (currentException != null)
            {
                if (!string.IsNullOrEmpty(sb.ToString()))
                {
                    sb.Append(delimeter);
                }
                sb.Append(currentException.Message);

                currentException = currentException.InnerException;
            }

            return sb.ToString();
        }

        private bool HasRecursedTooManyTimes(ExceptionContext filterContext, int recursionLimit = 3)
        {
            int? exceptionDepth = 0;
            if (filterContext.HttpContext.Items.Contains("ExceptionDepth"))
            {
                exceptionDepth = filterContext.HttpContext.Items["ExceptionDepth"] as int?;
                filterContext.HttpContext.Items["ExceptionDepth"] = exceptionDepth.HasValue
                    ? exceptionDepth + 1
                    : recursionLimit;
            }
            else
            {
                filterContext.HttpContext.Items["ExceptionDepth"] = 1;
            }

            if (exceptionDepth >= recursionLimit)
            {
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.Write(
                    "An error has occurred, but in trying to display it, another error has occurred.");
                return true;
            }

            return false;
        }
    }
}