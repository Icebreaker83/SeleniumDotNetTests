using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using SeleniumNetTests.Input.Data;
using SeleniumNetTests.Input.Data.Model;
using SeleniumNetTests.Input.Data.Node;
using SeleniumNetTests.Input.Data.PageElement;
using SeleniumNetTests.WebPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Input.DataProcessor
{
    public class InputDataProcessor
    {
        #region Private members

        List<DataNode> _inputDataNodes;

        List<DataNode> _outputDataNodes;

        List<PageElementNode> _pageElementNodes;

        NavigationNode _navigationNode;

        #endregion

        public InputDataProcessor(string testName, string testInputFile)
        {
            TestName = testName;
            TestInputFile = testInputFile;

            InitializeDataProcessor();
        }

        #region Private methods

        private void InitializeDataProcessor()
        {
            var dataModel = DataLoader.LoadInputData(TestName, TestInputFile);

            if (dataModel != null)
            {
                if (dataModel.NavigationNode != null)
                {
                    ProcessNavigationData(dataModel.NavigationNode);
                }

                if (dataModel.PageElementItems != null)
                {
                    foreach (PageElementItem element in dataModel.PageElementItems)
                    {
                        AddPageElementNode(new TabPageElementNode(
                            element.PageTabTitle,
                            element.PageTabIndex,
                            TestName, TestInputFile));
                    }
                }

                // Process input data nodes
                if (dataModel.DataNodes != null)
                {
                    ProcessInputDataNode(dataModel.DataNodes);
                }

                // Process output data nodes
                if (dataModel.OutputDataNodes != null)
                {
                    ProcessOutputDataNode(dataModel.OutputDataNodes);
                }

                DataLoaded = true;
            }
        }

        private void ProcessNavigationData(NavigationNodeModelItem navigationNode)
        {
            if (navigationNode.NavigationNodeValue != null)
            {
                switch (navigationNode.NavigationNodeType)
                {
                    case NavigationType.URL:
                        _navigationNode = new UrlNavigationNode(
                            navigationNode.NavigationNodeValue,
                            TestName, TestInputFile);
                        break;
                    case NavigationType.MENUITEM:
                        _navigationNode = new MenuItemNavigationNode(
                            navigationNode.NavigationNodeValue,
                            TestName, TestInputFile);
                        break;
                    case NavigationType.TABITEM:
                        _navigationNode = new TabItemNavigationNode(
                            navigationNode.NavigationNodeValue,
                            navigationNode.PageTabIndex.Value,
                            navigationNode.PageTabId,
                            TestName, TestInputFile);
                        break;
                    default:
                        Assert.IsTrue(false, $"{TestName} : {TestInputFile} - Unknown navigation node item found in input resource");
                        break;
                }
            }
        }

        private void ProcessInputDataNode(DataNodeModelItem[] dataNodes)
        {
            if (dataNodes != null)
            {
                foreach (DataNodeModelItem dataNodeItem in dataNodes)
                {
                    switch (dataNodeItem.DataNodeType)
                    {
                        case DataType.INPUT:
                            AddDataNode(new InputDataNode(
                                dataNodeItem.DataNodeId,
                                dataNodeItem.DataNodeValue,
                                dataNodeItem.DataNodeDescription,
                                TestName, TestInputFile));
                            break;
                        case DataType.FILE:
                            AddDataNode(new FileDataNode(
                                dataNodeItem.DataNodeId,
                                dataNodeItem.DataNodeValue,
                                dataNodeItem.DataNodeDescription,
                                TestName, TestInputFile));
                            break;
                        case DataType.SELECT:
                            AddDataNode(new SelectDataNode(
                                dataNodeItem.DataNodeId,
                                dataNodeItem.DataNodeValue,
                                dataNodeItem.DataWaitReload,
                                dataNodeItem.DataNodeDescription,
                                TestName, TestInputFile));
                            break;
                        case DataType.DATE:
                            break;
                        case DataType.CHECKBOX:
                            break;
                        case DataType.MULTIPLE:
                            break;
                        case DataType.RADIO:
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void ProcessOutputDataNode(DataNodeModelItem[] dataNodes)
        {
            if (dataNodes != null)
            {
                foreach (DataNodeModelItem dataNodeItem in dataNodes)
                {
                    switch (dataNodeItem.DataNodeType)
                    {
                        case DataType.INPUT:
                            AddOutputDataNode(new InputDataNode(
                                dataNodeItem.DataNodeId,
                                dataNodeItem.DataNodeValue,
                                dataNodeItem.DataNodeDescription,
                                TestName, TestInputFile));
                            break;
                        case DataType.FILE:
                            AddOutputDataNode(new FileDataNode(
                                dataNodeItem.DataNodeId,
                                dataNodeItem.DataNodeValue,
                                dataNodeItem.DataNodeDescription,
                                TestName, TestInputFile));
                            break;
                        case DataType.SELECT:
                            AddOutputDataNode(new SelectDataNode(
                                dataNodeItem.DataNodeId,
                                dataNodeItem.DataNodeValue,
                                dataNodeItem.DataWaitReload,
                                dataNodeItem.DataNodeDescription,
                                TestName, TestInputFile));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void AddDataNode(DataNode dataNode)
        {
            if (_inputDataNodes == null)
            {
                _inputDataNodes = new List<DataNode>();
            }

            _inputDataNodes.Add(dataNode);
        }

        private void AddOutputDataNode(DataNode dataNode)
        {
            if (_outputDataNodes == null)
            {
                _outputDataNodes = new List<DataNode>();
            }

            _outputDataNodes.Add(dataNode);
        }

        private void AddPageElementNode(PageElementNode pageElementNode)
        {
            if (_pageElementNodes == null)
            {
                _pageElementNodes = new List<PageElementNode>();
            }

            _pageElementNodes.Add(pageElementNode);
        }

        #endregion

        #region Properties

        public bool DataLoaded { get; private set; }

        public List<DataNode> InputDataNodes
        {
            get { return _inputDataNodes; }
        }

        public List<DataNode> OutputDataNodes
        {
            get { return _outputDataNodes; }
        }

        public List<PageElementNode> PageElementNodes
        {
            get { return _pageElementNodes; }
        }

        public NavigationNode NavigationNode
        {
            get { return _navigationNode; }
        }

        public string TestName { get; private set; }

        public string TestInputFile { get; private set; }
            
        #endregion
    }
}
