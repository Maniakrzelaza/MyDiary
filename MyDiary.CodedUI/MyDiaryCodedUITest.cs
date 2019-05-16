using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace MyDiary.CodedUI
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class MyDiaryCodedUITest
    {
        public MyDiaryCodedUITest()
        {
        }
  
       [TestMethod]
        public void ShouldHaveArticeDetailProperTitle()
        {
            this.UIMap.LoginToAdmin();
            this.UIMap.ArticleShouldHaveProperTitle();
            this.UIMap.ReturnToMainPage();
            this.UIMap.Logout();
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }
        [TestMethod]
        public void ShouldLoginAndLogout()
        {
            this.UIMap.LoginToAdmin();
            this.UIMap.CheckIfHelloBannerExists();
            this.UIMap.ReturnToMainPage();
            this.UIMap.Logout();
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }
        [TestMethod]
        public void ShouldAdminHasMailOnProfile()
        {
            this.UIMap.LoginToAdmin();
            this.UIMap.GoToProfilePanel();
            this.UIMap.CheckIfUserHasMailOnProfilePage();
            this.UIMap.GoToRootPageFromProfielPanel();
            this.UIMap.GoToArticleDetailsThenAddCommentThenBack();
            this.UIMap.CheckIfCommentIndexPageIsShown();
            this.UIMap.Logout();
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        [TestMethod]
        public void ShouldBeAbleToGetToAddCommentPanel()
        {
            this.UIMap.LoginToAdmin();
            this.UIMap.GoToArticleDetailsThenAddCommentThenBack();
            this.UIMap.CheckIfCommentIndexPageIsShown();
            this.UIMap.Logout();
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }
        [TestMethod]
        public void ShouldBeAbleToDeleteArticle()
        {
            this.UIMap.LoginToAdmin();
            this.UIMap.GoToArticleDelete();
            this.UIMap.ShouldHaveDeleteButton();
            this.UIMap.ReturnToMainPage();
            this.UIMap.Logout();
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }
        [TestMethod]
        public void ShouldBeAbleToDeleteArticleComment()
        {
            this.UIMap.LoginToAdmin();
            this.UIMap.GoToArticleCommentDelete();
            this.UIMap.ShouldHaveCommentDeleteButton();
            this.UIMap.ReturnToMainPage();
            this.UIMap.Logout();
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        [TestMethod]
        public void ShouldDisplayWronPasswordMessageWhenLoginCredencialsAreWrong()
        {
            this.UIMap.GoToLoginPage();
            this.UIMap.TypeWrongLoginCredentials();
            this.UIMap.ShouldDisplayInvalidLoginMessage();
            this.UIMap.ReturnToMainPage();
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }
        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if (this.map == null)
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
