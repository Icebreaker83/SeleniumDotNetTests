using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Common
{
    public class AssemblyInfo
    {
        public static string AssemblyDirectory
        {
            get
            {
                var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
                return Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
            }
        }

        public static string ResourceDirectory
        {
            get
            {
                return Path.Combine(AssemblyDirectory, "Resources");
            }
        }
    }
}
