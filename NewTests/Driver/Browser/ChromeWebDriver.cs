using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumNetTests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Driver
{
    public class ChromeWebDriver : WebDriver
    {
        public ChromeWebDriver(TestContext testContext) 
            : base(testContext) { }

        #region Method overrides

        protected override void InitializeWebDriver()
        {
            var driverService = ChromeDriverService.CreateDefaultService(AssemblyInfo.AssemblyDirectory);
            Driver = new ChromeDriver(driverService);

            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        #endregion

        #region Static properties

        public static string BrowserName { get; } = "Chrome";

        #endregion
    }
}
