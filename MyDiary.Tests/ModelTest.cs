using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using Moq;
using MyDiary.Data;
using MyDiary.Models;
using MyDiary.VIewModel;
using NUnit.Framework;
using MyDiary.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MyDiaryTests
{
    class ModelTest
    {
        NoBadWords sutNoBadWords;
        [SetUp]
        public void Setup()
        {
            sutNoBadWords = new NoBadWords("bad");
        }
        [Test]
        public void ShouldReturnSuccessWhenTestIsValid()
        {
            //Arrage
            Article article = new Article();
            article.Content = "There should not be any no good words";
            ValidationContext validationContext = new ValidationContext(article);
            Type typeNoBadWords = typeof(NoBadWords);
            object[] args = { null, validationContext };

            //Act
            ValidationResult result = (ValidationResult)typeNoBadWords.InvokeMember("IsValid",
            BindingFlags.DeclaredOnly |
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.InvokeMethod, null, sutNoBadWords, args);

            //Assert
            Assert.AreEqual(ValidationResult.Success, result);
        }
        [Test]
        public void ShouldReturnMessageWhenTestIsNotValid()
        {
            //Arrage
            Article article = new Article
            {
                Content = "There should not be any bad words"
            };
            ValidationContext validationContext = new ValidationContext(article);
            Type typeNoBadWords = typeof(NoBadWords);
            object[] args = { null, validationContext };

            //Act
            ValidationResult result = (ValidationResult)typeNoBadWords.InvokeMember("IsValid",
            BindingFlags.DeclaredOnly |
            BindingFlags.Public | BindingFlags.NonPublic |
            BindingFlags.Instance | BindingFlags.InvokeMethod, null, sutNoBadWords, args);

            //Assert
            Assert.AreEqual("Comment can not contain bad words.", result.ToString());
        }
        [Test]
        public void ShouldReturnFalseWhenTextIsNotLongerThan1Chars()
        {
            //Arrage
            Article article = new Article
            {
                Content = ""
            };
            StringLengthAttribute stringLengthValidator = new StringLengthAttribute(5000);
            stringLengthValidator.MinimumLength = 1;

            //Act
            bool result = stringLengthValidator.IsValid(article.Content);

            //Assert
            Assert.False(result);
        }
        [Test]
        public void ShouldReturnSuccesWhenTextIsLongerThan1Chars()
        {
            //Arrage
            Article article = new Article
            {
                Content = "Text should be longer than 1 char"
            };
            StringLengthAttribute stringLengthValidator = new StringLengthAttribute(5000);
            stringLengthValidator.MinimumLength = 1;

            //Act
            bool result = stringLengthValidator.IsValid(article.Content);

            //Assert
            Assert.True(result);
        }
        [Test]
        public void ShouldReturnSuccesWhenTextIsNoLongerThan5000Chars()
        {
            //Arrage
            Article article = new Article
            {
                Content = "Text should be longer than 1 char"
            };
            StringLengthAttribute stringLengthValidator = new StringLengthAttribute(5000);
            stringLengthValidator.MinimumLength = 1;

            //Act
            bool result = stringLengthValidator.IsValid(article.Content);

            //Assert
            Assert.True(result);
        }
        [Test]
        public void ShouldArticleTitleHaveFirstLetterUpper()
        {
            //Arrage
            Article article = new Article
            {
                Title = "first letter should be upper"
            };
            List<Article> resultArticles = new List<Article>();
            resultArticles.Add(article);
            var mockIAppDbContext = new Mock<IApplicationDbContext>();
            mockIAppDbContext.Setup(mock => mock.ArticlesSet)
                .Returns(resultArticles.ToAsyncEnumerable);
            var articlesController = ArticlesController.Of(mockIAppDbContext.Object);

            //Act
            var result = articlesController.VerifyTitle("first letter should be upper").Result;

            //Assert
            Assert.AreEqual("Title first letter should be upper first letter should be upper", ((JsonResult)result).Value.ToString());
        }
        [Test]
        public void ShouldArticleTitleBeUnique()
        {
            //Arrage
            Article article = new Article
            {
                Title = "First letter should be upper"
            };
            List<Article> resultArticles = new List<Article>();
            resultArticles.Add(article);
            var mockIAppDbContext = new Mock<IApplicationDbContext>();
            mockIAppDbContext.Setup(mock => mock.ArticlesSet)
                .Returns(resultArticles.ToAsyncEnumerable);
            var articlesController = ArticlesController.Of(mockIAppDbContext.Object);

            //Act
            var result = articlesController.VerifyTitle("First letter should be upper").Result;
            

            //Assert
            Assert.AreEqual(
                "Title First letter should be upper is already in use", 
                ((JsonResult)result).Value.ToString());
        }
    }
}
