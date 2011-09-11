using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using DotNetFlow.Features.Extensions;

namespace DotNetFlow.Features.Events
{
    [Binding]
    public class BrowserEvents
    {
        [AfterScenario]
        public static void Close()
        {
            ScenarioContext.Current.Dispose<IWebDriver>(browser =>
            {
                try
                {
                    browser.Quit();
                }
                catch (Exception)
                {
                    // Ignore exceptions when closing browser
                }
            });
        }
    }
}
