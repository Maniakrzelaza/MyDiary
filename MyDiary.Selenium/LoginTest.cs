using MyDiary.Selenium;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using System.Reflection;

namespace Tests
{
    public class LoginTest
    {
        private IWebDriver Driver;
        [SetUp]
        public void Setup()
        {
            FirefoxOptions options = new FirefoxOptions()
            {
                AcceptInsecureCertificates = true
            };
            options.AddArgument("--headless");
            Driver = new FirefoxDriver(Environment.CurrentDirectory + @"/drivers/", options);
        }

        [Test]
        public void ShouldHaveProperTitle()
        {
            Driver.Url = "https://127.0.0.1:5001/";
            Assert.AreEqual("Index - MyDiary", Driver.Title);
        }
        [Test]
        public void ShouldLogInSuccessfuly()
        {
            PageAccess.LogInToAdmin(Driver);
            IWebElement showLoginWhenLogged = Driver.FindElement(By.XPath("/html/body/header/nav/div/div/ul[1]/li[1]/a"));

            Assert.AreEqual("Hello admin@gmail.com!", showLoginWhenLogged.Text);
        }
        [Test]
        public void ShouldLogInAndLogOutSuccessfuly()
        {
            PageAccess.LogInToAdmin(Driver);
            PageAccess.LogOut(Driver);
            IWebElement loginButton = Driver.FindElement(By.CssSelector("li.nav-item:nth-child(2) > a:nth-child(1)"));

            Assert.AreEqual("Login", loginButton.Text);
        }
        [TearDown]
        public void TearDown()
        {
            Driver.Close();
        }
    }
}