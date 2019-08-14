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
    public class InputDataNode : DataNode
    {
        public InputDataNode(string dataNodeId, string dataNodeValue, string dataNodeDescription, string testName, string testInputFile)
            : base(dataNodeId, dataNodeValue, dataNodeDescription, testName, testInputFile) { }

        #region Method overrides

        public override void ProcessDataNode(IWebDriver webDriver, WebDriverWait webDriverWait)
        {
            var inputElement = FindElementById(webDriver);
            inputElement.Clear();
            inputElement.SendKeys(DataNodeValue);
        }

        public override void ValidateDataNode(IWebDriver webDriver, WebDriverWait webDriverWait)
        {
            var inputElement = FindElementById(webDriver);
            var inputValue = inputElement.GetAttribute("value");
            Assert.IsTrue(DataNodeValue.Equals(inputValue, StringComparison.InvariantCultureIgnoreCase), $"Node validation failed for element : {DataNodeDescription}, value : {DataNodeValue}");
        }

        #endregion

        #region Properties

        public override DataType DataNodeType { get; } = DataType.INPUT;

        #endregion
    }
}
