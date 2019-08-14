using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SeleniumNetTests.Driver.Extension
{
    public static class ElementWaitExtension
    {
        /// <summary>
        /// Checks whether alert is displayd on screen
        /// </summary>
        /// <param name="webDriver"></param>
        /// <returns></returns>
        public static bool AlertDisplayed(this IWebDriver webDriver)
        {
            try
            {
                webDriver.SwitchTo().Alert();
            }
            catch (NoAlertPresentException)
            {
                return false;
            }

            return true;
        }

        public static IWebElement WaitUntilElementActive(this IWebDriver webDriver, By by)
        {
            var elementActive = false;

            while (!elementActive)
            {
                try
                {
                    var element = webDriver.FindElement(by);
                    element.Click();

                    return element;
                }
                catch (StaleElementReferenceException)
                {
                    Thread.Sleep(50);
                }
                catch (ElementNotInteractableException)
                {
                    Thread.Sleep(50);
                }
                catch (WebDriverException)
                {
                    Thread.Sleep(50);
                }
                catch (NullReferenceException)
                {
                    Thread.Sleep(50);
                }
            }

            return null;
        }

        /// <summary>
        /// Checks whether alert is displayd on screen
        /// </summary>
        /// <param name="webDriver"></param>
        /// <returns></returns>
        public static bool DocumentReady(this IWebDriver webDriver)
        {
            try
            {
                webDriver.ExecuteJavascript("return document.readyState").Equals("complete");
            }
            catch (NoAlertPresentException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether alert is displayd on screen
        /// </summary>
        /// <param name="webDriver"></param>
        /// <returns></returns>
        public static bool FrameDocumentReady(this IWebDriver webDriver, string frameId)
        {
            try
            {
                webDriver.SwitchTo().DefaultContent();

                webDriver.ExecuteJavascript($@"
                    var iframe = document.getElementById('{frameId}');
                    var iframeDoc = iframe.contentDocument || iframe.contentWindow.document;    
                    return iframeDoc.readyState == 'complete'");

                webDriver.SwitchTo().Frame(webDriver.FindElement(By.Id(frameId)));
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
            finally
            {
                webDriver.SwitchTo().Frame(webDriver.FindElement(By.Id(frameId)));
            }

            return true;
        }

        /// <summary>
        /// Execute Javascript command without parameters
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="jsCommand"></param>
        public static object ExecuteJavascript(this IWebDriver webDriver, string jsCommand)
        {
            var js = (IJavaScriptExecutor)webDriver;
            return js.ExecuteScript(jsCommand);
        }

        /// <summary>
        /// Execute javascript command with parameters
        /// </summary>
        /// <param name="webDriver"></param>
        /// <param name="jsCommand"></param>
        /// <param name="jsParameters"></param>
        public static object ExecuteJavascript(this IWebDriver webDriver, string jsCommand, params object[] jsParameters)
        {
            var js = (IJavaScriptExecutor)webDriver;
            return js.ExecuteScript(jsCommand, jsParameters);
        }
    }
}
