using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using SeleniumNetTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Driver
{
    public class FirefoxWebDriver : WebDriver
    {
        public FirefoxWebDriver(TestContext testContext) 
            : base(testContext) { }

        #region Method overrides

        protected override void InitializeWebDriver()
        {
            var driverService = FirefoxDriverService.CreateDefaultService(AssemblyInfo.AssemblyDirectory);
            Driver = new FirefoxDriver(driverService);

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        #endregion

        #region Static properties

        public static string BrowserName { get; } = "Firefox";

        #endregion
    }
}
