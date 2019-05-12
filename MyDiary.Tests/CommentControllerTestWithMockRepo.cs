using Moq;
using MyDiary.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MyDiary.Data;
using MyDiary.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Principal;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;

namespace MyDiaryTests
{
    class CommentControllerTestWithMockRepo
    {
        [Test]
        public void ShouldCallFindCommentByIdWhenEditWasCalled()
        {
            //Arrage
            Comment resultComment = new Comment() { Content = "TestContent" };
            var mockIAppDbContext = new Mock<IApplicationDbContext>();
            mockIAppDbContext.Setup(mock => mock.FindCommentById(It.IsAny<int>()))
                .Returns(Task.FromResult(resultComment));
            var sutCommentController = CommentsController.Of(mockIAppDbContext.Object);

            //Act
            var editView = sutCommentController.Edit(1).Result as ViewResult;

            //Assert/Verify
            mockIAppDbContext.Verify(
                mock => mock.FindCommentById(It.IsAny<int>()), Times.Once());
            Assert.AreEqual("TestContent", ((Comment)(editView.Model)).Content);
        }
        [Test]
        public void ShouldCallAddToDbContextWhenCreateWasCalled()
        {
            //Arrage
            List<Comment> resultComments = new List<Comment>();
            var resultComment = new Comment() { Id = 1, Content = "TestContent1" };
            resultComments.Add(new Comment() { Content = "TestContent1" });
            resultComments.Add(new Comment() { Content = "TestContent2" });
            resultComments.Add(new Comment() { Content = "TestContent3" });
            var mockIAppDbContext = new Mock<IApplicationDbContext>();
            mockIAppDbContext.Setup(mock => mock.Add(It.IsAny<Comment>()));
            mockIAppDbContext.Setup(mock => mock.FindArticleById(It.IsAny<int>()))
                .Returns(Task.FromResult(new Article()));
            var sutCommentController = CommentsController.Of(mockIAppDbContext.Object);
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }));

            sutCommentController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            //Act
            var editView = sutCommentController.Create(resultComment, 1).Result as ViewResult;

            //Assert/Verify
            mockIAppDbContext.Verify(
                mock => mock.Add(It.IsAny<Comment>()), Times.Once());
            mockIAppDbContext.Verify(
                mock => mock.FindArticleById(It.IsAny<int>()), Times.Once());
        }
        [Test]
        public void ShouldCallUpdateWhenArgsAreProperAndEditWasCalled()
        {
            //Arrage
            Comment resultComment = new Comment() { Id = 1, Content = "TestContent" };
            var mockIAppDbContext = new Mock<IApplicationDbContext>();
            mockIAppDbContext.Setup(mock => mock.Update(It.IsAny<Comment>()));
            var sutCommentController = CommentsController.Of(mockIAppDbContext.Object);

            //Act
            var editView = sutCommentController.Edit(1, resultComment).Result as ViewResult;

            //Assert/Verify
            mockIAppDbContext.Verify(
                mock => mock.Update(It.IsAny<Comment>()), Times.Once());
        }
        [Test]
        public void ShouldHandleDbException()
        {
            //Arrage
            Comment resultComment = new Comment() {Id = 1, Content = "TestContent" };
            List<Comment> resultComments = new List<Comment>();
            resultComments.Add(resultComment);
            var mockIAppDbContext = new Mock<IApplicationDbContext>();
            var entries = new List<IUpdateEntry>();
            entries.Add(new Mock<IUpdateEntry>().Object);
            mockIAppDbContext.Setup(mock => mock.SaveChangesAsync())
                .ThrowsAsync(new DbUpdateConcurrencyException("Concurrency error", entries));
            mockIAppDbContext.Setup(mock => mock.CommentsSet)
               .Returns(resultComments.ToAsyncEnumerable);
            var sutCommentController = CommentsController.Of(mockIAppDbContext.Object);

            //Act
            DbUpdateConcurrencyException e = Assert.ThrowsAsync<DbUpdateConcurrencyException>(
                async () => await sutCommentController.Edit(1, resultComment));

            //Assert/Verify
            mockIAppDbContext.Verify(
                mock => mock.SaveChangesAsync(), Times.Once());
            Assert.AreEqual("Concurrency error", e.Message.ToString());
        }
    }
}
