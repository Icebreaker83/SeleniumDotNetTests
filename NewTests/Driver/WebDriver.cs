using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Driver
{
    public abstract class WebDriver : IDisposable
    {
        #region Private members

        bool _webDriverDisposed = false;

        #endregion

        public WebDriver(TestContext testContext)
        {
            TestContext = testContext;

            InitializeWebDriver();

            InitializeWebDriverWait();
        }

        #region Abstract methods

        protected abstract void InitializeWebDriver();

        #endregion

        #region Private methods

        private void InitializeWebDriverWait()
        {
            if (Driver == null) { throw new NotImplementedException("Web driver not initialized properly"); }

            DriverWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
        }

        #endregion

        #region Properties

        public IWebDriver Driver { get; protected set; }

        public WebDriverWait DriverWait { get; protected set; }

        public TestContext TestContext { get; private set; }

        #endregion

        #region Dispose support

        protected virtual void Dispose(bool disposing)
        {
            if (!_webDriverDisposed)
            {
                if (disposing)
                {
                    Driver.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                _webDriverDisposed = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }
}
