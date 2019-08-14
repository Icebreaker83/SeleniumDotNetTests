using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Input.Data
{
    public abstract class NavigationNode : InputNode
    {
        #region Private members

        readonly string[] _navigationNodeValue;

        #endregion

        public NavigationNode(string[] navigationNodeValue, string testName, string inputTestFile)
            : base(testName, inputTestFile)
        {
            _navigationNodeValue = navigationNodeValue;
        }

        #region Abstract methods

        public abstract void Navigate(IWebDriver webDriver, WebDriverWait webDriverWait);

        #endregion

        #region Abstract properties

        public abstract NavigationType NavigationType { get; }

        #endregion

        #region Properties

        protected string[] NavigationValue { get { return _navigationNodeValue; } }

        #endregion
    }

    public enum NavigationType
    {
        URL = 1,
        MENUITEM,
        TABITEM
    }
}
