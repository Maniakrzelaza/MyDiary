using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDiary.Selenium
{
    class CommentTest
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
        public void ShoulGoToCommentDeleteButtonAndBackToCommentsPage()
        {
            PageAccess.LogInToAdmin(Driver);
            IWebElement firstArticleDetails = Driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/table/tbody/tr[1]/td[2]/a[3]"));
            firstArticleDetails.Click();
            IWebElement commentDeleteButton = Driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/div[3]/table/tbody/tr[1]/td[3]/a[2]"));
            commentDeleteButton.Click();
            IWebElement backToArticlesPageButton = Driver.FindElement(By.CssSelector(".main-content > div:nth-child(3) > form:nth-child(4) > a:nth-child(3)"));
            backToArticlesPageButton.Click();
            IWebElement commentLabel = Driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/h1"));
            Assert.AreEqual("My Comments", commentLabel.Text);
        }
        [Test]
        public void ShoulCommentDeleteButtonHaveProperLink()
        {
            PageAccess.LogInToAdmin(Driver);
            IWebElement firstArticleDetails = Driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/table/tbody/tr[1]/td[2]/a[3]"));
            firstArticleDetails.Click();
            IWebElement commentDeleteButton = Driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/div[3]/table/tbody/tr[1]/td[3]/a[2]"));
            commentDeleteButton.Click();
            IWebElement deleteLink = Driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/div/form"));
            String deleteActionLink = deleteLink.GetAttribute("action");
            Assert.AreEqual("https://127.0.0.1:5001/Comments/Delete/31", deleteActionLink);
        }
        [TearDown]
        public void TearDown()
        {
            Driver.Close();
        }
    }
}
