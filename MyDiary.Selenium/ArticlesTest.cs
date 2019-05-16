using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyDiary.Selenium
{
    class ArticlesTest
    {
        private IWebDriver Driver;
        [SetUp]
        public void Setup()
        {
            FirefoxOptions options = new FirefoxOptions()
            {
                AcceptInsecureCertificates = true
            };
           // options.AddArgument("--headless");
            Driver = new FirefoxDriver(Environment.CurrentDirectory + @"/drivers/", options);
            Driver.Url = "https://127.0.0.1:5001/";
        }

        [Test]
        public void ShouldHaveArticlesWithGreaterThan0Size()
        {
            ReadOnlyCollection<IWebElement> articles = Driver
                .FindElements(By.ClassName("article-row"));
            Assert.That(articles, Has.Count.GreaterThan(0));
        }

        [Test]
        public void ShouldHaveElementWithTitle()
        {
            Driver.Url = "https://127.0.0.1:5001/";
            ReadOnlyCollection<IWebElement> articles = Driver
                .FindElements(By.ClassName("article-row"));
            IWebElement firstArticle = articles[0].FindElement(By.XPath("//*[contains(@class,'article-title')] "));
            Assert.AreEqual("Life underground", firstArticle.Text);
        }
        
        [TearDown]
        public void TearDown()
        {
            Driver.Close();
        }
    }
}
