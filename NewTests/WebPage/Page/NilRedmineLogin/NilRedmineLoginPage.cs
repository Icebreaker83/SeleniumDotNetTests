using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumNetTests.Driver;
using SeleniumNetTests.Driver.Extension;
using SeleniumNetTests.Input.Data;
using SeleniumNetTests.Input.DataProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.WebPages.Page.NilRedmineLogin
{
    public class NilRedmineLoginPage : WebPage
    {
        public NilRedmineLoginPage(WebDriver webDriver, string testName)
            : base(webDriver, testName) { }

        #region Method overrides

        protected override void ValidateInitialState()
        {
            // Nothing to validate - initial test
        }

        protected override void ValidatePageLoadedState()
        {
            _driverWait.Until(c => SignInHeaderElement.Displayed);
        }

        #endregion

        #region Public methods

        public NilRedmineLoginPage SignInConfirmClick()
        {
            _driver.TryFindElement(SignInButtonBy, "Button 'Login'").Click();
            _driverWait.Until(c => MainMenuElement.Displayed);

            return this;
        }

        #endregion

        #region Properties

        IWebElement SignInHeaderElement
        {
            get
            {
                return _driver.FindElement(By.Id("header"));
            }
        }

        IWebElement MainMenuElement
        {
            get
            {
                return _driver.FindElement(By.Id("content"));
            }
        }

        /// <summary>
        /// Button 'Login'
        /// </summary>
        By SignInButtonBy { get; } = By.Id("login-submit");

        #endregion

        #region Property overrides

        protected override string PageInput { get; } = "nilRedmineLogin";

        #endregion
    }
}
