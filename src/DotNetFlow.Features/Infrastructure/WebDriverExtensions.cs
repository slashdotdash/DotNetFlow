using System;
using System.Collections.Generic;
using System.Linq;
using DotNetFlow.Features.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DotNetFlow.Features
{
    public static class WebDriverExtensions
    {
        public const int DefaultTimeout = 10;

        public static void Wait(this IWebDriver driver, int seconds = DefaultTimeout)
        {
            if (seconds <= 60)
                seconds *= 1000;

            System.Threading.Thread.Sleep(seconds);
        }

        /// <summary>
        /// Wait until all jQuery AJAX requests have completed
        /// </summary>
        public static void WaitForAjaxRequests(this IWebDriver driver, int abortAfterNumberOfSecodns = DefaultTimeout)
        {
            var wait = new WebDriverWait(WebBrowser.Current, TimeSpan.FromSeconds(abortAfterNumberOfSecodns));
            wait.Until(browser =>
            {
                var pendingAjaxRequests = 0;
                var jQueryPendingRequests = browser.GetJavaScriptExecutor().ExecuteScript("return window.jQuery.active").ToString();

                if (int.TryParse(jQueryPendingRequests, out pendingAjaxRequests))
                {
                    return pendingAjaxRequests == 0;
                }

                return true;
            });
        }

        public static IWebElement FindElement(this IWebDriver driver, By by, Func<IWebElement, bool> predicate)
        {
            return driver.FindElements(by, predicate).First();
        }

        public static IEnumerable<IWebElement> FindElements(this IWebDriver driver, By by, Func<IWebElement, bool> predicate)
        {
            return driver.FindElements(by).Where(predicate);
        }

        public static IWebElement WaitForElement(this IWebDriver driver, By by, Func<IWebElement, bool> predicate = null, int seconds = DefaultTimeout)
        {
            return driver.WaitForElements(by, predicate, seconds).First();
        }

        public static IEnumerable<IWebElement> WaitForElements(this IWebDriver driver, By by, Func<IWebElement, bool> predicate = null, int seconds = DefaultTimeout)
        {
            IEnumerable<IWebElement> els;
            var retry = 0;

            do
            {
                retry++;
                driver.Wait(1);

                els = driver.FindElements(by);
                if (predicate != null)
                    els = els.Where(predicate);

            } while (els.Count() == 0 && retry < seconds);

            return els;
        }
        
        public static IJavaScriptExecutor GetJavaScriptExecutor(this IWebDriver driver)
        {
            return driver as IJavaScriptExecutor;
        }
    }    
}
