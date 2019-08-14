using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Input.Data
{
    public abstract class InputNode
    {
        #region Private members

        readonly string _testName;

        readonly string _testInputFile;

        #endregion

        public InputNode (string testName, string testInputFile)
        {
            _testName = testName;
            _testInputFile = testInputFile;
        }

        #region Virtual methods

        protected virtual string InputResource
        {
            get
            {
                return $"Test : {_testName}, Resource : {_testInputFile}";
            }
        }

        #endregion

        #region Protected properties

        protected string TestName { get { return _testName; } }

        #endregion
    }
}
