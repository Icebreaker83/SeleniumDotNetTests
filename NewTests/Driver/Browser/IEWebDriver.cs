using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using SeleniumNetTests.Common;
using SeleniumNetTests.Driver.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Driver
{
    public class IEWebDriver : WebDriver
    {
        public IEWebDriver(TestContext testContext) 
            : base(testContext) { }

        #region Method overrides

        protected override void InitializeWebDriver()
        {
            var driverOptions = new InternetExplorerOptions
            {
                IgnoreZoomLevel = true,
                EnableNativeEvents = false,
                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                EnablePersistentHover = true,
                RequireWindowFocus = true
            };

            var driverService = InternetExplorerDriverService.CreateDefaultService(AssemblyInfo.AssemblyDirectory);
            Driver = new InternetExplorerDriver(driverService, driverOptions);

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
        }

        #endregion

        #region Static properties

        public static string BrowserName { get; } = "InternetExplorer";

        #endregion
    }
}
