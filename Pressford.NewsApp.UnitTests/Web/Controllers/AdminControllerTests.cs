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
    public class AdminControllerTests
    {
        private Mock<IArticleService> articleServiceMock;
        private Mock<IMapper> mapperMock;
        private Mock<IUserIdentity> userIdentityMock;
        private AdminController adminController;

        [TestInitialize]
        public void Init()
        {
            articleServiceMock = new Mock<IArticleService>();
            mapperMock = new Mock<IMapper>();
            userIdentityMock = new Mock<IUserIdentity>();
            adminController = new AdminController(articleServiceMock.Object, mapperMock.Object, userIdentityMock.Object);
        }

        [TestMethod]
        public void AdminController_Constructor_IsNull_Should_Throw_ArgumentNullException()
        {
            Action action = () => new AdminController(null, null, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task Dashboard_When_Called_Should_Return_ViewResult()
        {
            articleServiceMock.Setup(x => x.GetAllArticles()).Returns(MockData.TestArticlesMock());
            var result = await adminController.Dashboard() as ViewResult;
            result.Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<DashboardViewModel>();
        }

        [TestMethod]
        public void CreateArticle_When_Called_Should_Return_ViewResult()
        {         
            var result = adminController.CreateArticle() as ViewResult;
            result.Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<ArticleViewModel>();
        }

        [TestMethod]
        public async Task CreateArticle_When_ArticleViewModel_HasErrors_Should_Return_ViewResult()
        {
            adminController.ModelState.AddModelError("", "error");

            var result = await adminController.CreateArticle(new ArticleViewModel()) as ViewResult;

            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public async Task CreateArticle_When_ArticleViewModel_HasNoErrors_Should_Return_ViewResult_Dashboard()
        {
            mapperMock.Setup(x => x.Map<ArticleViewModel, Article>(It.IsAny<ArticleViewModel>()))
                      .Returns(MockData.TestArticleMock());

            userIdentityMock.Setup(x => x.GetName()).Returns("test user");

            articleServiceMock.Setup(x => x.GetAllArticles()).Returns(MockData.TestArticlesMock());
                   
            var result = await adminController.CreateArticle(new ArticleViewModel()) as ViewResult;

            result.Should().BeOfType<ViewResult>().Which.ViewName.Should().Be("Dashboard");         
        }

        [TestMethod]
        public async Task EditArticle_When_Called_Should_Return_ViewResult()
        {
            mapperMock.Setup(x => x.Map<ArticleViewModel, Article>(It.IsAny<ArticleViewModel>()))
                      .Returns(MockData.TestArticleMock());

            int id = 1;
            var result = await adminController.EditArticle(id) as ViewResult;
            result.Should().BeOfType<ViewResult>();
        }
        
        [TestMethod]
        public async Task EditArticle_When_ArticleViewModel_HasErrors_Should_Return_ViewResult()
        {
            adminController.ModelState.AddModelError("", "error");

            var result = await adminController.EditArticle(new ArticleViewModel()) as ViewResult;

            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public async Task EditArticle_When_ArticleViewModel_HasNoErrors_Should_RedirectToAction_Dashboard()
        {
            mapperMock.Setup(x => x.Map<ArticleViewModel, Article>(It.IsAny<ArticleViewModel>()))
                      .Returns(MockData.TestArticleMock());                    

            var result = await adminController.EditArticle(new ArticleViewModel()) as RedirectToActionResult;

            result.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Dashboard");
        }

        [TestMethod]
        public async Task DeleteArticle_When_Called_Should_RedirectToAction_Dashboard()
        {
            int id = 1;
            var result = await adminController.DeleteArticle(id) as RedirectToActionResult;
            result.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Dashboard");
        }
    }
}
