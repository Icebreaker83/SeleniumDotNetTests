using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumNetTests.Driver.Extension;

namespace SeleniumNetTests.Input.Data.Node
{
    public class MenuItemNavigationNode : NavigationNode
    {        
        public MenuItemNavigationNode(string[] navigationNodeValue, string testName, string testInputFile)
            : base(navigationNodeValue, testName, testInputFile) { }

        #region Method overrides

        public override void Navigate(IWebDriver webDriver, WebDriverWait webDriverWait)
        {
            foreach (string navigationMenuItem in NavigationValue)
            {
                webDriverWait.Until(c => c.FindElement(By.XPath($"//a[text() = '{navigationMenuItem}']")).Enabled);
                var menuItem = webDriver.TryFindElement(By.XPath($"//a[text() = '{navigationMenuItem}']"), navigationMenuItem);

                webDriver.ExecuteJavascript("arguments[0].click();", menuItem);
            }
        }

        #endregion

        #region Properties

        public override NavigationType NavigationType { get; } = NavigationType.MENUITEM;

        #endregion
    }
}
