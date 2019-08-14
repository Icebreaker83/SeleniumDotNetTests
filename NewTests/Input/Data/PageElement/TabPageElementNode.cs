using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Input.Data.PageElement
{
    public class TabPageElementNode : PageElementNode
    {
        #region Private members

        readonly string _pageTabTitle;

        readonly int _pageTabIndex;

        #endregion

        public TabPageElementNode(string pageTabTitle, int pageTabIndex, string testName, string testInputFile)
            : base(testName, testInputFile)
        {
            _pageTabTitle = pageTabTitle;
            _pageTabIndex = pageTabIndex;
        }
    }
}
