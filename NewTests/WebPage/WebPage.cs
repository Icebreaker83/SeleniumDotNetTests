using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumNetTests.Driver;
using SeleniumNetTests.Input.Data;
using SeleniumNetTests.Input.DataProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.WebPages
{
    public abstract class WebPage
    {
        #region Private members

        protected readonly IWebDriver _driver;

        protected readonly WebDriverWait _driverWait;

        protected readonly TestContext _testContext;

        protected readonly string _testName;

        #endregion

        #region Private members

        readonly InputDataProcessor _inputDataProcessor;

        bool _dataLoaded = false;

        #endregion

        public WebPage(WebDriver webDriver, string testName, WebPageInputProcessing inputProcessing = WebPageInputProcessing.Immediate)
        {
            _driver = webDriver.Driver;
            _driverWait = webDriver.DriverWait;
            _testContext = webDriver.TestContext;
            _testName = testName;

            ValidateInitialState();

            _inputDataProcessor = new InputDataProcessor(testName, PageInput);

            if (inputProcessing == WebPageInputProcessing.Immediate)
            {
                // Navigate to test page
                ProcessNavigateData();

                // Process input data for page
                ProcessInputData();
            }
        }

        #region Public methods

        /// <summary>
        /// Switch focus to current web page
        /// </summary>
        public void ActivateWebPage()
        {
            BringToFocus();

            ProcessNavigateData();
        }

        /// <summary>
        /// Load and process data for current tab
        /// </summary>
        public void ProcessWebPageData()
        {
            if (!_dataLoaded)
            {
                ProcessInputData();
                _dataLoaded = true;
            }
        }

        /// <summary>
        /// Process and load all output validation page data from input resource
        /// </summary>
        /// <param name="inputDataNodes"></param>
        public void ValidateWebPageData()
        {
            var outputNodes = _inputDataProcessor.OutputDataNodes;

            Assert.IsTrue(outputNodes != null || outputNodes.Count() > 0, $"{_inputDataProcessor.TestName} : {_inputDataProcessor.TestInputFile} - Output validation data not initialized");

            foreach (DataNode dataNode in outputNodes)
            {
                dataNode.ValidateDataNode(_driver, _driverWait);
            }

            // Add additional input validation
            AddCustomDataValidation(_inputDataProcessor.OutputDataNodes);
        }

        public void CloseWebPage(WebPageType webPageType = WebPageType.Standard)
        {
            ClosePage();

            if (webPageType == WebPageType.Popup)
            {
                RestoreFocus();
            }
        }

        #endregion

        #region Private methods

        private void ProcessNavigateData()
        {
            if (_inputDataProcessor == null) { return; }

            Assert.IsTrue(_inputDataProcessor.DataLoaded, $"{_inputDataProcessor.TestName} : {_inputDataProcessor.TestInputFile} - Input data not initialized");
            NavigateToPage(_inputDataProcessor.NavigationNode);

            ValidatePageLoadedState();
        }

        private void ProcessInputData()
        {
            if (_inputDataProcessor == null) { return; }

            Assert.IsTrue(_inputDataProcessor.DataLoaded, $"{_inputDataProcessor.TestName} : {_inputDataProcessor.TestInputFile} - Input data not initialized");
            ProcessPageData(_inputDataProcessor.InputDataNodes);
        }

        private void BringToFocus()
        {
            _driverWait.Until(c => c.WindowHandles.Count > 1);

            foreach (var handle in _driver.WindowHandles)
            {
                if (!_driver.CurrentWindowHandle.Equals(handle, StringComparison.InvariantCultureIgnoreCase))
                {
                    _driver.SwitchTo().Window(handle);
                    return;
                }
            }
        }

        private void RestoreFocus()
        {
            _driverWait.Until(c => c.WindowHandles.Count == 1);

            //_driver.SwitchTo().ActiveElement();
        }

        #endregion

        #region Virtual methods

        /// <summary>
        /// Validate initial test state before loading the current page
        /// </summary>
        protected virtual void ValidateInitialState()
        {
            // do nothing in abstract class
        }

        /// <summary>
        /// Custom page elements initialization
        /// </summary>
        /// <param name="navigationNode"></param>
        protected virtual void InitializePageElements(NavigationNode navigationNode)
        {
            // do nothing in abstract class
        }

        /// <summary>
        /// Validate test state after navigation to page
        /// </summary>
        protected virtual void ValidatePageLoadedState()
        {
            // do nothing in abstract class
        }

        /// <summary>
        /// Validate test state after navigation to page
        /// </summary>
        protected virtual void ClosePage()
        {
            // do nothing in abstract class
        }

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

        /// <summary>
        /// Process and load all page data from input resource
        /// </summary>
        /// <param name="inputDataNodes"></param>
        protected virtual void AddCustomDataValidation(List<DataNode> outputDataNodes)
        {
            // do nothing in abstract class
        }

        #endregion

        #region Abstract properties

        protected abstract string PageInput { get; }

        #endregion
    }

    public enum WebPageInputProcessing
    {
        Immediate = 1,
        Skip
    }

    public enum WebPageType
    {
        Standard = 1,
        Tab,
        Popup
    }
}
