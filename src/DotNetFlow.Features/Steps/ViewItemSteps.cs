using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Steps
{
    [Binding]
    public class ViewItemSteps
    {
        [Then(@"I should see the published item")]
        public void ThenIShouldSeeThePublishedItem()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
