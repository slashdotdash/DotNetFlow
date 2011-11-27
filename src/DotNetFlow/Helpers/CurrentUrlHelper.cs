using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DotNetFlow.Helpers
{
    public static class CurrentUrlHelper
    {
        public static string CurrentUrlCss<TModel>(this HtmlHelper<TModel> helper, string url, string currentPageCssClass = "current-page")
        {
            return helper.ViewContext.RequestContext.HttpContext.Request.RawUrl == url
                       ? " " + currentPageCssClass
                       : string.Empty;
        }

        public static Uri UrlOriginal(this HttpRequestBase request)
        {
            var hostHeader = request.Headers["host"];

            return new Uri(string.Format("{0}://{1}{2}",
               request.Url.Scheme,
               hostHeader,
               request.RawUrl));
        }

    }
}