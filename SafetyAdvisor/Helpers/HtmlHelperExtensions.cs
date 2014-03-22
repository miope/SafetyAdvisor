using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SafetyAdvisor.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static bool IsController(this HtmlHelper html, string controllerName)
        {
            return (html.ViewContext.RouteData.Values["controller"] as string).ToLower() == controllerName.ToLower();
        }

        public static bool IsAction(this HtmlHelper html, string actionName)
        {
            return (html.ViewContext.RouteData.Values["action"] as string).ToLower() == actionName.ToLower();
        }

        public static bool IsControllerAndAction(this HtmlHelper html, string controllerName, string actionName)
        {
            return html.IsController(controllerName) && html.IsAction(actionName);
        }

        public static MvcHtmlString CssClassIfControllerAndAction(this HtmlHelper html, string controllerName, string actionName, string cssClass)
        {
            return MvcHtmlString.Create(html.IsControllerAndAction(controllerName, actionName) ? string.Format("class=\"{0}\"", cssClass) : string.Empty);
        }

        public static MvcHtmlString LinkToReferrer(this HtmlHelper @this, string text, MvcHtmlString falbackUrl)
        {
            var _referrer = @this.ViewContext.RequestContext.HttpContext.Request.UrlReferrer;
            return _referrer != null ? MvcHtmlString.Create(string.Format("<a href=\"{0}\">{1}</a>", _referrer.ToString(), text)) : falbackUrl;
        }
    }
}