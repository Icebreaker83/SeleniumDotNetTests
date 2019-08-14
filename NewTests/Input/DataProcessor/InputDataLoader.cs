using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using SeleniumNetTests.Common;
using SeleniumNetTests.Input.Data.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Input
{
    public class DataLoader
    {
        public static DataNodeModel LoadInputData(string testPath, string testInputFile)
        {
            try
            {
                var filePath = Path.Combine(AssemblyInfo.ResourceDirectory, testPath, testInputFile + ".json");

                Assert.IsTrue(File.Exists(filePath), $"{testPath} : {testInputFile} - Input file not found");

                using (var file = File.OpenText(filePath))
                {
                    var serializer = new JsonSerializer();
                    return (DataNodeModel)serializer.Deserialize(file, typeof(DataNodeModel));
                }
            }
            catch(Exception ex)
            {
                Assert.IsFalse(true, $"{testPath} : {testInputFile} - {ex.Message}");
                return null;
            }
        }
    }
}
