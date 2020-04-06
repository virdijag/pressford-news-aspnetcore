using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Service.Interfaces;
using Pressford.NewsApp.UnitTests.CommonMockData;
using Pressford.NewsApp.Web.Controllers;
using Pressford.NewsApp.Web.Security;
using Pressford.NewsApp.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pressford.NewsApp.UnitTests.Web.Controllers
{
    [TestClass]
    public class AppControllerTests
    {
        private Mock<IArticleService> articleServiceMock;
        private Mock<ICommentService> commentServiceMock;
        private Mock<IMapper> mapperMock;
        private Mock<IUserIdentity> userIdentityMock;
        private AppController appController;

        [TestInitialize]
        public void Init()
        {
            articleServiceMock = new Mock<IArticleService>();
            commentServiceMock = new Mock<ICommentService>();
            mapperMock = new Mock<IMapper>();
            userIdentityMock = new Mock<IUserIdentity>();
            appController = new AppController(articleServiceMock.Object, commentServiceMock.Object, mapperMock.Object, userIdentityMock.Object);
        }

        [TestMethod]
        public void AppController_Constructor_IsNull_Should_Throw_ArgumentNullException()
        {
            Action action = () => new AppController(null, null, null, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task Home_When_Called_Should_Return_ViewResult()
        {
            articleServiceMock.Setup(x => x.GetAllArticles()).Returns(MockData.TestArticlesMock());
            var result = await appController.Home() as ViewResult;
            result.Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<HomeViewModel>();
        }

        [TestMethod]
        public async Task Article_When_Called_Should_Return_ViewResult()
        {
            mapperMock.Setup(x => x.Map<Article, ArticleViewModel>(It.IsAny<Article>()))
                      .Returns(MockData.TestArticleViewModelMock());

            commentServiceMock.Setup(x => x.GetArticleComments(It.IsAny<int>())).Returns(MockData.TestArticleCommentsMock());
           
            mapperMock.Setup(x => x.Map<List<Comment>, List<CommentViewModel>>(It.IsAny<List<Comment>>()))
                     .Returns(new List<CommentViewModel>());

            int articleId = 1;
            var result = await appController.Article(articleId) as ViewResult;

            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public async Task AddArticleLike_When_ArticleViewModel_HasNoErrors_Should_RedirectToAction_Article()
        {
            mapperMock.Setup(x => x.Map<ArticleViewModel, Article>(It.IsAny<ArticleViewModel>()))
                      .Returns(MockData.TestArticleMock());

            int limitCount = 1;
            userIdentityMock.Setup(x => x.GetLikeLimit()).Returns(limitCount);

            var result = await appController.AddArticleLike(new ArticleViewModel()) as RedirectToActionResult;

            result.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Article");
        }

        [TestMethod]
        public async Task AddComment_When_ArticleViewModel_HasNoErrors_Should_RedirectToAction_Article()
        {
            mapperMock.Setup(x => x.Map<CommentViewModel, Comment>(It.IsAny<CommentViewModel>()))
                      .Returns(MockData.TestArticleCommentMock());

            var result = await appController.AddComment(new ArticleViewModel()) as RedirectToActionResult;

            result.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Article");
        }
    }
}
