using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyDiary.Selenium
{
    class PageAccess
    {
        public static void GetToRootPage(IWebDriver driver)
        {
            driver.Url = "https://127.0.0.1:5001/";
        }
        public static void LogOut(IWebDriver driver)
        {
            IWebElement logOutButton = driver.FindElement(By.XPath("/html/body/header/nav/div/div/ul[1]/li[2]/form/button"));
            logOutButton.Click();
        }
        public static void LogInToAdmin(IWebDriver driver)
        {
            driver.Url = @"https:/127.0.0.1:5001/Identity/Account/Login";
            IWebElement email = driver.FindElement(By.Id("Input_Email"));
            email.SendKeys("admin@gmail.com");
            IWebElement password = driver.FindElement(By.Id("Input_Password"));
            password.SendKeys("nhy6%TGB");
            IWebElement submitButton = driver.FindElement(By.XPath("/html/body/div/main/div[2]/div/div/div[1]/section/form/div[5]/button"));
            submitButton.Click();
        }
    }
}
