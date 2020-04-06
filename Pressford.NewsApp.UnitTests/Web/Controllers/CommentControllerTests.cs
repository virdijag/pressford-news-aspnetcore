using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Service.Interfaces;
using Pressford.NewsApp.UnitTests.CommonMockData;
using Pressford.NewsApp.Web.Controllers;
using Pressford.NewsApp.Web.ViewModels;
using System;
using System.Threading.Tasks;

namespace Pressford.NewsApp.UnitTests.Web.Controllers
{
    [TestClass]
    public class CommentControllerTests
    {
        private Mock<ICommentService> commentServiceMock;
        private Mock<IMapper> mapperMock;
        private CommentController commentController;

        [TestInitialize]
        public void Init()
        {
            commentServiceMock = new Mock<ICommentService>();
            mapperMock = new Mock<IMapper>();
        
            commentController = new CommentController(commentServiceMock.Object, mapperMock.Object);
        }

        [TestMethod]
        public void CommentController_Constructor_IsNull_Should_Throw_ArgumentNullException()
        {
            Action action = () => new CommentController(null, null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task Get_ById_When_Called_Should_Return_OkObjectResult()
        {
            commentServiceMock.Setup(x => x.GetArticleComments(It.IsAny<int>())).Returns(MockData.TestArticleCommentsMock());
            int id = 1;
            var result = await commentController.Get(id) as OkObjectResult;
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task Post_When_Called_With_CommentViewModel_HasNoErrors_Should_Return_OkResult()
        {
            mapperMock.Setup(x => x.Map<CommentViewModel, Comment>(It.IsAny<CommentViewModel>()))
                      .Returns(MockData.TestArticleCommentMock());

            var result = await commentController.Post(new CommentViewModel()) as OkResult;

            result.Should().BeOfType<OkResult>().Which.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task Post_When_Called_With_CommentViewModel_HasErrors_Should_Return_BadRequestObjectResult()
        {
            commentController.ModelState.AddModelError("", "error");

            mapperMock.Setup(x => x.Map<CommentViewModel, Comment>(It.IsAny<CommentViewModel>()))
                      .Returns(MockData.TestArticleCommentMock());

            var result = await commentController.Post(new CommentViewModel()) as BadRequestObjectResult;

            result.Should().BeOfType<BadRequestObjectResult>().Which.StatusCode.Should().Be(400);
        }
    }
}
