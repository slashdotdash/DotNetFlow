using System.Web;
using System.Web.Mvc;

namespace DotNetFlow.Extensions
{
    /// <summary>
    /// Provides flash message functionality similar to a Ruby on Rails application, using cookies.
    /// </summary>
    /// <see cref="http://github.com/bcalloway/jquery-flash-message"/>
    internal static class FlashMessageExtensions
    {
        public static ActionResult Notice(this ActionResult result, string message)
        {
            CreateCookieWithFlashMessage(Notification.Notice, message);
            return result;
        }

        public static ActionResult Warning(this ActionResult result, string message)
        {
            CreateCookieWithFlashMessage(Notification.Warning, message);
            return result;
        }

        public static ActionResult Message(this ActionResult result, string message)
        {
            CreateCookieWithFlashMessage(Notification.Message, message);
            return result;
        }

        private static void CreateCookieWithFlashMessage(Notification notification, string message)
        {
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(string.Format("Flash.{0}", notification), message) { Path = "/" });
        }

        private enum Notification
        {
            Notice,
            Warning,
            Message
        }
    }
}