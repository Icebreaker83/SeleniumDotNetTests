using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Input.Data.Model
{
    public class DataNodeModel
    {
        public NavigationNodeModelItem NavigationNode { get; set; }

        public PageElementItem[] PageElementItems { get; set; }

        public DataNodeModelItem[] DataNodes { get; set; }

        public DataNodeModelItem[] OutputDataNodes { get; set; }
    }

    public class DataNodeModelItem
    {
        public string DataNodeId { get; set; }

        public string DataNodeValue { get; set; }

        public DataType DataNodeType { get; set; }

        public bool DataWaitReload { get; set; } = false;

        public string DataNodeDescription { get; set; }
    }

    public class NavigationNodeModelItem
    {
        public string[] NavigationNodeValue { get; set; }

        public NavigationType NavigationNodeType { get; set; }

        public int? PageTabIndex { get; set; }

        public string PageTabId { get; set; }
    }

    public class PageElementItem
    {
        public int PageTabIndex { get; set; }

        public string PageTabTitle { get; set; }
    }
}
