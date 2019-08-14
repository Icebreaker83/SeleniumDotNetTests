using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumNetTests.Common;

namespace SeleniumNetTests.Input.Data.Node
{
    public class FileDataNode : DataNode
    {
        public FileDataNode(string dataNodeName, string dataNodeValue, string dataNodeDescription, string testName, string testInputFile)
            : base(dataNodeName, dataNodeValue, dataNodeDescription, testName, testInputFile) { }

        #region Method overrides

        public override void ProcessDataNode(IWebDriver webDriver, WebDriverWait webDriverWait)
        {
            var filePath = Path.Combine(AssemblyInfo.ResourceDirectory, TestName, DataNodeValue);

            var fileInputElement = FindElementById(webDriver);
            fileInputElement.Clear();
            fileInputElement.SendKeys(filePath);
        }

        public override void ValidateDataNode(IWebDriver webDriver, WebDriverWait webDriverWait)
        {
            var inputElement = FindElementById(webDriver);
            var inputValue = inputElement.GetAttribute("value");
            Assert.IsTrue(DataNodeValue.Equals(inputValue, StringComparison.InvariantCultureIgnoreCase), $"Node validation failed for element : {DataNodeDescription}, value : {DataNodeValue}");
        }

        #endregion

        #region Properties

        public override DataType DataNodeType { get; } = DataType.FILE;

        #endregion
    }
}
