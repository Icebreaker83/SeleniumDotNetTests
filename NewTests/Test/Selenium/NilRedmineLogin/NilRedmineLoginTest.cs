using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.UI;
using SeleniumNetTests.Driver;
using SeleniumNetTests.Input.DataProcessor;
using SeleniumNetTests.WebPages.Page;
using SeleniumNetTests.WebPages.Page.NilRedmineLogin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumNetTests.Test.Selenium.NilRedmineLogin
{
    [TestClass]
    public class LoginTests : SeleniumTest
    {
        [TestMethod, Timeout(60000)]
        public void TestNilRedmineLogin()
        {
            using (var webDriver = WebDriverService.Service.CreateWebDriver(TestContext))
            {
                var loginPage = new NilRedmineLoginPage(webDriver, TestName);

                loginPage.SignInConfirmClick();
            }
        }

        #region Properties override

        public override string TestName { get; } = "NilRedmineLogin";

        #endregion
    }
}
