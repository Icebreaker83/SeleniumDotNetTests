using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Driver
{
    public class WebDriverService
    {
        static WebDriverService _browserServiceInstance = null;

        private WebDriverService() { }

        #region Public methods

        public WebDriver CreateWebDriver(TestContext testContext)
        {
            var defaultWebBrowser = testContext.Properties["defaultTestBrowser"];
            Assert.IsTrue(defaultWebBrowser != null, "Missing default browser configuration setting 'defaultTestBrowser'");

            if (string.IsNullOrEmpty(defaultWebBrowser.ToString())) { defaultWebBrowser = DefaultWebBrowser; }

            if (defaultWebBrowser.ToString().Equals(FirefoxWebDriver.BrowserName, StringComparison.InvariantCultureIgnoreCase))
            {
                return new FirefoxWebDriver(testContext);
            }
            else if (defaultWebBrowser.ToString().Equals(ChromeWebDriver.BrowserName, StringComparison.InvariantCultureIgnoreCase))
            {
                return new ChromeWebDriver(testContext);
            } else if (defaultWebBrowser.ToString().Equals(IEWebDriver.BrowserName, StringComparison.InvariantCultureIgnoreCase))
            {

                return new IEWebDriver(testContext);
            }
            else
            {
                Assert.IsTrue(false, "Browser driver not supported");
                return null;
            }
        }

        #endregion

        #region Properties

        public static WebDriverService Service
        {
            get
            {
                if (_browserServiceInstance == null)
                {
                    _browserServiceInstance = new WebDriverService();
                }

                return _browserServiceInstance;
            }
        }
        
        private static string DefaultWebBrowser { get; } = ChromeWebDriver.BrowserName;
        
        #endregion
    }
}
