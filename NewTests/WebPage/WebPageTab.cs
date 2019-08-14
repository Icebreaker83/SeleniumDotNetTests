using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumNetTests.Driver;
using SeleniumNetTests.Input.Data;
using SeleniumNetTests.Input.Data.Node;
using SeleniumNetTests.Input.DataProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.WebPages
{
    public abstract class WebPageTab
    {
        #region Protected members

        protected readonly IWebDriver _driver;

        protected readonly WebDriverWait _driverWait;

        protected readonly TestContext _testContext;

        #endregion

        #region Private members

        readonly InputDataProcessor _inputDataProcessor;

        bool _dataLoaded = false;

        #endregion

        public WebPageTab(WebDriver webDriver, string testName, string pageInput)
        {
            _driver = webDriver.Driver;
            _driverWait = webDriver.DriverWait;
            _testContext = webDriver.TestContext;

            _inputDataProcessor = new InputDataProcessor(testName, pageInput + TabInputSuffix);
        }

        #region Public methods

        /// <summary>
        /// Switch focus to current tab
        /// </summary>
        public void ActivateTab()
        {
            ProcessNavigateData();
        }

        /// <summary>
        /// Load and process data for current tab
        /// </summary>
        public void ProcessTabData()
        {
            if (!_dataLoaded)
            {
                ProcessInputData();
                _dataLoaded = true;
            }
        }

        #endregion

        #region Private methods

        private void ProcessNavigateData()
        {
            if (_inputDataProcessor == null) { return; }

            Assert.IsTrue(_inputDataProcessor.DataLoaded, $"{_inputDataProcessor.TestName} : {_inputDataProcessor.TestInputFile} - Input data not initialized");
            Assert.IsNotNull(_inputDataProcessor.NavigationNode, $"{_inputDataProcessor.TestName} : {_inputDataProcessor.TestInputFile} - Navigate node not found");
            NavigateToPage(_inputDataProcessor.NavigationNode);

            ValidatePageLoadedState();
        }

        private void ProcessInputData()
        {
            if (_inputDataProcessor == null) { return; }

            Assert.IsTrue(_inputDataProcessor.DataLoaded, $"{_inputDataProcessor.TestName} : {_inputDataProcessor.TestInputFile} - Input data not initialized");

            ValidatePageLoadedState();
            ProcessPageData(_inputDataProcessor.InputDataNodes);
        }

        private void ValidatePageLoadedState()
        {
            var tabItemNavigationNode = _inputDataProcessor.NavigationNode as TabItemNavigationNode;
            _driverWait.Until(c => SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id($"{tabItemNavigationNode.TabId}")));
        }

        #endregion

        #region Virtual methods

        /// <summary>
        /// Default page navigation, override in derived class if
        /// specific navigation is requried
        /// </summary>
        /// <param name="navigationNode"></param>
        protected virtual void NavigateToPage(NavigationNode navigationNode)
        {
            if (navigationNode == null) { return; }

            navigationNode.Navigate(_driver, _driverWait);
        }

        /// <summary>
        /// Process and load all page data from input resource
        /// </summary>
        /// <param name="inputDataNodes"></param>
        protected virtual void ProcessPageData(List<DataNode> inputDataNodes)
        {
            if (inputDataNodes == null || inputDataNodes.Count() == 0) { return; }

            foreach (DataNode dataNode in inputDataNodes)
            {
                dataNode.ProcessDataNode(_driver, _driverWait);
            }
        }

        #endregion

        #region Abstract properties

        /// <summary>
        /// Append suffix to parent web page to create valid
        /// input data resource name
        /// </summary>
        public abstract string TabInputSuffix { get; }

        #endregion

        #region Protected properties

        protected bool DataLoaded { get { return _dataLoaded; } }

        #endregion

        #region Properties

        string PageIdentifier => this.GetType().Name;

        #endregion
    }
}
