using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Input.Data
{
    public abstract class DataNode : InputNode
    {
        #region Private members

        readonly string _dataNodeId;

        readonly string _dataNodeValue;

        readonly string _dataNodeDescription;

        #endregion

        public DataNode(string dataNodeId, string dataNodeValue, string dataNodeDescription, string testName, string testInputFile)
            : base(testName, testInputFile)
        {
            Assert.IsTrue(!string.IsNullOrEmpty(dataNodeId), $"{InputResource} - Invalid node ID input");
            _dataNodeId = dataNodeId;

            Assert.IsTrue(!string.IsNullOrEmpty(dataNodeValue), $"{InputResource} - Invalid node value input");
            _dataNodeValue = dataNodeValue;

            Assert.IsTrue(!string.IsNullOrEmpty(dataNodeValue), $"{InputResource} - Invalid node description");
            _dataNodeDescription = dataNodeDescription;
        }

        #region Protected methods

        protected IWebElement FindElementById(IWebDriver driver)
        {
            try
            {
                return driver.FindElement(By.Id(_dataNodeId));
            }
            catch (NoSuchElementException ex)
            {
                Assert.IsTrue(false, $"Error retrieving node : {_dataNodeDescription} - {ex.Message}");
                return null;
            }
        }

        #endregion

        #region Abstract methods

        public abstract void ProcessDataNode(IWebDriver webDriver, WebDriverWait webDriverWait);

        public abstract void ValidateDataNode(IWebDriver webDriver, WebDriverWait webDriverWait);

        #endregion

        #region Abstract properties

        public abstract DataType DataNodeType { get; }

        #endregion

        #region Properties

        protected string DataNodeValue { get { return _dataNodeValue; } }

        protected string DataNodeId { get { return _dataNodeId; } }

        protected string DataNodeDescription { get { return _dataNodeDescription; } }

        protected string DataFrameId { get; } = "iframe1";

        #endregion
    }

    public enum DataType
    {
        INPUT = 1,
        FILE,
        SELECT,
        DATE,
        CHECKBOX,
        MULTIPLE,
        RADIO
    }
}
