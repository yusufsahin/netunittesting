using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.CodeCoverage;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;
namespace SportsStore.TestsUI
{
    public class SuiteTests : IDisposable
    {
        public IWebDriver driver { get; private set; }
        public IDictionary<String, Object> vars { get; private set; }
        public IJavaScriptExecutor js { get; private set; }

        public SuiteTests()
        {
            driver = new ChromeDriver();
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<String, Object>();
        }

        public void Dispose()
        {
            driver.Quit();
        }

        [Fact]
        public void BasicSportsStoreOrder()
        {
            driver.Navigate().GoToUrl("http://sportsstore.innovium.net/");
            driver.Manage().Window.Maximize();

            ScrollToElement(By.LinkText("Home"));
            driver.FindElement(By.LinkText("Home")).Click();

            ScrollToElement(By.XPath("//button[@type='submit']"));
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            ScrollToElement(By.CssSelector("h2"));
            driver.FindElement(By.CssSelector("h2")).Click();

            WaitForElement(By.XPath("//h2[contains(.,'Your cart')]"));

            ScrollToElement(By.LinkText("Checkout"));
            driver.FindElement(By.LinkText("Checkout")).Click();

            ScrollToElement(By.CssSelector("h2"));
            driver.FindElement(By.CssSelector("h2")).Click();

            WaitForElement(By.XPath("//h2[contains(.,'Check out now')]"));

            driver.FindElement(By.Id("Name")).Click();
            driver.FindElement(By.Id("Name")).SendKeys("John Doe");
            driver.FindElement(By.Id("Line1")).Click();
            driver.FindElement(By.Id("Line1")).SendKeys("Silicon Hill Blv");
            driver.FindElement(By.CssSelector(".form-group:nth-child(5)")).Click();
            driver.FindElement(By.Id("Line2")).Click();
            driver.FindElement(By.Id("Line2")).SendKeys("No 9");
            driver.FindElement(By.Id("Line3")).Click();
            driver.FindElement(By.Id("Line3")).SendKeys("6785");
            driver.FindElement(By.Id("City")).Click();
            driver.FindElement(By.Id("City")).SendKeys("Austin");
               ScrollToElement(By.Id("State"));
            
            driver.FindElement(By.Id("State")).Click();
         
            driver.FindElement(By.Id("State")).SendKeys("TX");
            ScrollToElement(By.Id("Zip"));
            driver.FindElement(By.Id("Zip")).Click();
            driver.FindElement(By.Id("Zip")).SendKeys("56422");
            ScrollToElement(By.Id("Country"));
            driver.FindElement(By.Id("Country")).Click();
            driver.FindElement(By.Id("Country")).SendKeys("USA");

            ScrollToElement(By.CssSelector(".btn-primary"));
            driver.FindElement(By.CssSelector(".btn-primary")).Click();

            ScrollToElement(By.CssSelector("h2"));
            driver.FindElement(By.CssSelector("h2")).Click();

            WaitForElement(By.CssSelector("h2"));

            driver.FindElement(By.CssSelector("p:nth-child(3)")).Click();
            Assert.Equal(driver.FindElement(By.CssSelector("p:nth-child(3)")).Text, @"We'll ship your goods as soon as possible.");

            ScrollToElement(By.LinkText("Return to Store"));
            driver.FindElement(By.LinkText("Return to Store")).Click();
        }

        private void ScrollToElement(By by)
        {
            IWebElement element = driver.FindElement(by);
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }

        private void WaitForElement(By by)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(driver => driver.FindElement(by).Displayed);
        }
    }
}
