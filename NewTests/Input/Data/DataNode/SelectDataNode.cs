using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumNetTests.Driver.Extension;

namespace SeleniumNetTests.Input.Data.Node
{
    public class SelectDataNode : DataNode
    {
        #region Private members

        readonly bool _dataWaitReload;

        #endregion

        public SelectDataNode(string dataNodeName, string dataNodeValue, bool dataWaitReload, string dataNodeDescription, string testName, string testInputFile)
            : base(dataNodeName, dataNodeValue, dataNodeDescription, testName, testInputFile)
        {
            _dataWaitReload = dataWaitReload;
        }

        #region Method overrides

        public override void ProcessDataNode(IWebDriver webDriver, WebDriverWait webDriverWait)
        {
            webDriverWait.Until(
                c =>
                {
                    new SelectElement(FindElementById(webDriver)).SelectByText(DataNodeValue);
                    return c;
                });

            if (_dataWaitReload)
            {
                webDriver.WaitUntilElementActive(By.Id(DataNodeId));
            }
        }

        public override void ValidateDataNode(IWebDriver webDriver, WebDriverWait webDriverWait)
        {
            var inputElement = new SelectElement(FindElementById(webDriver));
            var inputValue = inputElement.SelectedOption.Text;

            Assert.IsTrue(DataNodeValue.Equals(inputValue, StringComparison.InvariantCultureIgnoreCase), $"Node validation failed for element : {DataNodeDescription}, value : {DataNodeValue}");
        }


        #endregion

        #region Properties

        public override DataType DataNodeType { get; } = DataType.SELECT;

        #endregion
    }
}
