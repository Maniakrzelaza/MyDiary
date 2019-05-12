using Moq;
using MyDiary.Models;
using NUnit.Framework;
using MyDiaryTests;
using MyDiary.Controllers;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyDiary.Data;
using System.Collections.Generic;
using MyDiary.VIewModel;

namespace MyDiaryTests
{
    class CommentControllerTestWithFakeRepo
    {
        CommentsController sutController;
        [SetUp]
        public void Setup()
        {
            sutController = CommentsController.Of(new FakeApplicationDbContext());
        }

        [Test]
        public void ShouldHaveProperSizeOfCommentsInModelIndexWasCalled()
        {
            //Arrage
            var fakeApplicationDbContext = new FakeApplicationDbContext();
            fakeApplicationDbContext.CommentsSet = new[] {
                new Comment(),
                new Comment(),
                new Comment(),
                new Comment()
            }.ToAsyncEnumerable();
            var sutController = CommentsController.Of(fakeApplicationDbContext);

            //Act
            var result = sutController.Index().Result as ViewResult;

            //Assert
            Assert.AreEqual(4, ((IEnumerable<Comment>)(result.Model)).Count());
        }
        [Test]
        public void ShouldHaveProperNameOfViewWhenIndexWasCalled()
        {
            //Arrage
            var fakeApplicationDbContext = new FakeApplicationDbContext();
            fakeApplicationDbContext.CommentsSet = new[] { new Comment() }
            .ToAsyncEnumerable();
            var sutController = CommentsController.Of(fakeApplicationDbContext);

            //Act
            var result = sutController.Index().Result as ViewResult;

            //Assert
            Assert.AreEqual("Index", result.ViewName);
        }
        [Test]
        public void ShouldReturnDetailsOfCommentDetailsWasCalled()
        {
            //Arrage
            var fakeApplicationDbContext = new FakeApplicationDbContext();
            fakeApplicationDbContext.CommentsSet = new[] {
                new Comment() {Id = 0, Content = "ContestTest1"},
                new Comment() {Id = 1, Content = "ContestTest2"},
                new Comment() {Id = 2, Content = "ContestTest3"}
            }.ToAsyncEnumerable();
            var sutController = CommentsController.Of(fakeApplicationDbContext);

            //Act
            var result = sutController.Details(1).Result as ViewResult;

            //Assert
            Assert.AreEqual("ContestTest2", ((Comment)(result.Model)).Content);
        }
        [Test]
        public void ShouldRetrunViewWithProperViewModelWhenCreateWasCalled()
        {
            //Arrage in setup
            //Act
            var result = sutController.Create(1) as ViewResult;

            //Assert
            Assert.AreEqual(typeof(ArticleCommentViewModel), result.Model.GetType());
        }
        [Test]
        public void ShouldDeleteCommentWhenDeleteConfirmationWasCalled()
        {
            //Arrage
            var fakeApplicationDbContext = new FakeApplicationDbContext();
            fakeApplicationDbContext.CommentsSet = new[] {
                new Comment() {Id = 0, Content = "ContestTest1"},
                new Comment() {Id = 1, Content = "ContestTest2"},
                new Comment() {Id = 2, Content = "ContestTest3"}
            }.ToAsyncEnumerable();
            var sutController = CommentsController.Of(fakeApplicationDbContext);

            //Act
            var result = sutController.DeleteConfirmed(1).Result as ViewResult;

            //Assert
            Assert.AreEqual(2, fakeApplicationDbContext.CommentsSet.ToEnumerable().Count());
        }
        [Test]
        public void ShouldThrowArgumentExceptionWhenDeleteWasCalledAndThereIsNoItemWithThatIndex()
        {
            //Arrage
            var fakeApplicationDbContext = new FakeApplicationDbContext();
            fakeApplicationDbContext.CommentsSet = new[] {
                new Comment() {Id = 0, Content = "ContestTest1"},
                new Comment() {Id = 1, Content = "ContestTest2"},
                new Comment() {Id = 2, Content = "ContestTest3"}
            }.ToAsyncEnumerable();
            var sutController = CommentsController.Of(fakeApplicationDbContext);

            //Act
            InvalidOperationException e = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await sutController.Delete(5));

            //Assert
            Assert.That(e.Message, Is.EqualTo("Sequence contains no elements"));
        }
        [Test]
        public void ShouldThrowNullArgumentExceptionWhenDetailsWasCalled()
        {
            //Arrage
            var fakeApplicationDbContext = new FakeApplicationDbContext();
            var sutController = CommentsController.Of(fakeApplicationDbContext);

            //Act
            ArgumentNullException e = Assert.ThrowsAsync<ArgumentNullException>(
                async () => await sutController.Details(null));

            //Assert
            Assert.That(e.Message, Is.EqualTo("Value cannot be null."));
        }
    }
}
