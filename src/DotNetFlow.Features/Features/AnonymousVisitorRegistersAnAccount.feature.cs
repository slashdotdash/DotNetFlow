﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.8.1.0
//      SpecFlow Generator Version:1.8.0.0
//      Runtime Version:4.0.30319.239
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace DotNetFlow.Features.Features
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.8.1.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Anonymous visitor registers an account")]
    public partial class AnonymousVisitorRegistersAnAccountFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "AnonymousVisitorRegistersAnAccount.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Anonymous visitor registers an account", "In order to gain reputation within the site\r\nAs a visitor to the site\r\nI want to " +
                    "register an account", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Successful registration")]
        public virtual void SuccessfulRegistration()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Successful registration", ((string[])(null)));
#line 6
  this.ScenarioSetup(scenarioInfo);
#line 7
    testRunner.Given("I am on the registration page");
#line 8
 testRunner.When("I complete the required fields for registration");
#line 9
 testRunner.And("I press \"Register Now\"");
#line 10
 testRunner.Then("I should be on the home page");
#line 11
 testRunner.And("I should be logged in");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Attempts to register with an already registered email")]
        public virtual void AttemptsToRegisterWithAnAlreadyRegisteredEmail()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Attempts to register with an already registered email", ((string[])(null)));
#line 14
  this.ScenarioSetup(scenarioInfo);
#line 15
    testRunner.Given("a user account has been registered");
#line 16
 testRunner.And("I am on the registration page");
#line 17
 testRunner.When("I attempt to register an account with an existing email address");
#line 18
 testRunner.And("I press \"Register Now\"");
#line 19
 testRunner.Then("I should see the error message \"Email address has already been registered\"");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion