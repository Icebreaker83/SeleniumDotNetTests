using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumNetTests.Input.Data.Node
{
    public class UrlNavigationNode : NavigationNode
    {        
        public UrlNavigationNode(string[] navigationNodeValue, string testName, string testInputFile)
            : base(navigationNodeValue, testName, testInputFile) { }

        #region Method overrides

        public override void Navigate(IWebDriver webDriver, WebDriverWait webDriverWait)
        {
            // Check if node is present
            Assert.IsTrue(NavigationValue.Count() == 1, $"{InputResource} - Invalid input navigation node");

            var navigationNode = NavigationValue.First();

            Uri uriResult = null;
            Assert.IsTrue(Uri.TryCreate(navigationNode, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps),
                $"{InputResource} - Navigation node is not a valid URI");

            webDriver.Navigate().GoToUrl(NavigationValue.First());
        }

        #endregion

        #region Properties

        public override NavigationType NavigationType { get; } = NavigationType.URL;

        #endregion
    }
}
