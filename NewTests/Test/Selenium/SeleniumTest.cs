using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Test.Selenium
{
    [TestClass]
    public abstract class SeleniumTest
    {
        /// <summary>
        /// Current test context, holding selenium configuration data.
        /// </summary>
        public TestContext TestContext { get; set; }

        #region Abstract properties

        /// <summary>
        /// Current test name
        /// <para>
        /// Used to designate test resource input folder
        /// under $(TargetDir)\Resources output folder
        /// </para>
        /// </summary>
        public abstract string TestName { get; }

        #endregion
    }
}
