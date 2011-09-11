using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using DotNetFlow.Features.Extensions;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Infrastructure
{
    [Binding]
    public class WebBrowser
    {
        public static IWebDriver Current
        {
            get { return ScenarioContext.Current.GetOrDefault(CreateWebDriver); }
        }

        private static IWebDriver CreateWebDriver()
        {
            return Firefox();
        }

        private static IWebDriver InternetExplorer()
        {
            var capabilities = DesiredCapabilities.InternetExplorer();
            return new InternetExplorerDriver(capabilities);
        }

        private static IWebDriver Firefox()
        {
            return new FirefoxDriver();
        }
    }
}