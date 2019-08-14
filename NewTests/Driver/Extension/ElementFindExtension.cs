using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumNetTests.Driver.Extension
{
    public static class ElementFindExtension
    {
        public static IWebElement TryFindElement(this IWebDriver driver, By by, string elementDescription)
        {
            try
            {
                return driver.FindElement(by);
            }
            catch (NoSuchElementException ex)
            {
                Assert.IsTrue(false, $"Error retrieving element : {elementDescription}, by : {by.ToString()} - {ex.Message}");
                return null;
            }
        }
    }
}
