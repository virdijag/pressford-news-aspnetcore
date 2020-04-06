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
using System.Threading.Tasks;

namespace Pressford.NewsApp.UnitTests.Web.Controllers
{
    [TestClass]
    public class ArticleControllerTests
    {
        private Mock<IArticleService> articleServiceMock;
        private Mock<IMapper> mapperMock;
        private Mock<IUserIdentity> userIdentityMock;
        private ArticleController articleController;

        [TestInitialize]
        public void Init()
        {
            articleServiceMock = new Mock<IArticleService>();
            mapperMock = new Mock<IMapper>();
            userIdentityMock = new Mock<IUserIdentity>();
            articleController = new ArticleController(articleServiceMock.Object, mapperMock.Object, userIdentityMock.Object);
        }

        [TestMethod]
        public void ArticleController_Constructor_IsNull_Should_Throw_ArgumentNullException()
        {
            Action action = () => new ArticleController(null, null, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task Get_When_Called_Should_Return_OkObjectResult()
        {
            articleServiceMock.Setup(x => x.GetAllArticles()).Returns(MockData.TestArticlesMock());
            var result = await articleController.Get() as OkObjectResult;
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task Get_ById_When_Called_Should_Return_OkObjectResult()
        {
            articleServiceMock.Setup(x => x.GetAllArticles()).Returns(MockData.TestArticlesMock());
            int id = 1;
            var result = await articleController.Get() as OkObjectResult;
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task Post_When_Called_With_ArticleViewModel_HasNoErrors_Should_Return_OkResult()
        {
            mapperMock.Setup(x => x.Map<ArticleViewModel, Article>(It.IsAny<ArticleViewModel>()))
                      .Returns(MockData.TestArticleMock());

            var result = await articleController.Post(new ArticleViewModel()) as OkResult;

            result.Should().BeOfType<OkResult>().Which.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task Post_When_Called_With_ArticleViewModel_HasErrors_Should_Return_BadRequestObjectResult()
        {
            articleController.ModelState.AddModelError("", "error");

            mapperMock.Setup(x => x.Map<ArticleViewModel, Article>(It.IsAny<ArticleViewModel>()))
                      .Returns(MockData.TestArticleMock());

            var result = await articleController.Post(new ArticleViewModel()) as BadRequestObjectResult;

            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }

        [TestMethod]
        public async Task Put_When_Called_With_ArticleViewModel_HasNoErrors_Should_Return_OkResult()
        {
            mapperMock.Setup(x => x.Map<ArticleViewModel, Article>(It.IsAny<ArticleViewModel>()))
                      .Returns(MockData.TestArticleMock());

            userIdentityMock.Setup(x => x.GetName()).Returns("testuser");

            var result = await articleController.Put(new ArticleViewModel()) as OkResult;

            result.Should().BeOfType<OkResult>().Which.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task Put_When_Called_With_ArticleViewModel_HasErrors_Should_Return_BadRequestObjectResult()
        {
            articleController.ModelState.AddModelError("", "error");

            mapperMock.Setup(x => x.Map<ArticleViewModel, Article>(It.IsAny<ArticleViewModel>()))
                      .Returns(MockData.TestArticleMock());

            var result = await articleController.Put(new ArticleViewModel()) as BadRequestObjectResult;

            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }

        [TestMethod]
        public async Task Delete_ById_When_Called_Should_Return_OkResult()
        {            
            int id = 1;
            var result = await articleController.Delete(id) as OkResult;
            result.Should().BeOfType<OkResult>().Which.StatusCode.Should().Be(200);
        }
    }
}
