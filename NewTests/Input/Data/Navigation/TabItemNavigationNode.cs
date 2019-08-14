using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumNetTests.Driver.Extension;

namespace SeleniumNetTests.Input.Data.Node
{
    public class TabItemNavigationNode : NavigationNode
    {
        #region Private members

        readonly int _tabIndex;

        readonly string _tabId;

        #endregion

        public TabItemNavigationNode(string[] navigationNodeValue, int tabIndex, string tabId, string testName, string testInputFile)
            : base(navigationNodeValue, testName, testInputFile)
        {
            _tabIndex = tabIndex;
            _tabId = tabId;
        }

        #region Method overrides

        public override void Navigate(IWebDriver webDriver, WebDriverWait webDriverWait)
        {
            webDriverWait.Until(c => c.DocumentReady());
            webDriver.ExecuteJavascript($"tabPane1.setSelectedIndex({_tabIndex});");

            webDriver.WaitUntilElementActive(By.Id($"{_tabId}"));
            webDriverWait.Until(c => webDriver.FindElement(By.Id($"{_tabId}")).Displayed);
        }

        #endregion

        #region Properties override

        public override NavigationType NavigationType { get; } = NavigationType.TABITEM;

        #endregion

        #region Properties

        public string TabId { get { return _tabId; } }

        #endregion
    }
}
