using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDiary.Selenium
{
    class AppTest
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
        }
        [Test]
        public void CrawlThroughtArticleEndPoint()
        {
            List<string> titleList = new List<string>();
            PageAccess.LogInToAdmin(Driver);
            Driver.Url = "https://127.0.0.1:5001/Articles";
            titleList.Add(Driver.Title);
            Driver.Url = "https://127.0.0.1:5001/Articles/Edit/9";
            titleList.Add(Driver.Title);
            Driver.Url = "https://127.0.0.1:5001/Articles/Delete/9";
            titleList.Add(Driver.Title);
            Driver.Url = "https://127.0.0.1:5001/Articles/Details/9";
            titleList.Add(Driver.Title);
            Driver.Url = "https://127.0.0.1:5001/Articles/Create";
            titleList.Add(Driver.Title);
            Assert.That(titleList, Is.All.Contains("MyDiary"));
        }
        [Test]
        public void CrawlThroughtCommentEndPoint()
        {
            List<string> titleList = new List<string>();
            PageAccess.LogInToAdmin(Driver);
            Driver.Url = "https://localhost:5001/Comments";
            titleList.Add(Driver.Title);
            Driver.Url = "https://localhost:5001/Comments/Edit/2";
            titleList.Add(Driver.Title);
            Driver.Url = "https://localhost:5001/Comments/Details/2";
            titleList.Add(Driver.Title);
            Driver.Url = "https://localhost:5001/Comments/Delete/2";
            titleList.Add(Driver.Title);
            Driver.Url = "https://localhost:5001/Comments/Create";
            titleList.Add(Driver.Title);
            Assert.That(titleList, Is.All.Contains("MyDiary"));
        }
        [TearDown]
        public void TearDown()
        {
            Driver.Close();
        }
    }
}
