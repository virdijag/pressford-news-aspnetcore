using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Repository.Interfaces;
using Pressford.NewsApp.Service;
using Pressford.NewsApp.Service.Interfaces;
using Pressford.NewsApp.UnitTests.CommonMockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pressford.NewsApp.UnitTests
{
    [TestClass]
    public class CommentServiceTests
    {
        private Mock<ICommentRepository> commentRespositoryMock;
        private ICommentService commentService;

        [TestInitialize]
        public void Init()
        {
            commentRespositoryMock = new Mock<ICommentRepository>();
            commentService = new CommentService(commentRespositoryMock.Object);
        }

        [TestMethod]
        public void CommentService_Constructor_IsNull_Should_Throw_ArgumentNullException()
        {
            Action action = () => new CommentService(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void AddComment_Verify_RepositoryMethod_AddComment_IsCalledOnce()
        {
            Comment comment = MockData.TestArticleCommentMock();

            commentService.AddComment(comment);

            commentRespositoryMock.Verify(x => x.AddComment(It.IsAny<Comment>()), Times.Once);
        }

        [TestMethod]
        public void GetArticleComments_Verify_RepositoryMethod_GetArticleComments_IsCalledOnce()
        {
            int articleId = 1;

            commentService.GetArticleComments(articleId);

            commentRespositoryMock.Verify(x => x.GetArticleComments(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task GetArticleComments_WhenCalled_Returns_ArticleComments()
        {
            int articleId = 1;
            commentRespositoryMock.Setup(x => x.GetArticleComments(articleId)).Returns(MockData.TestArticleCommentsMock());

            IEnumerable<Comment> articleComments = await commentService.GetArticleComments(articleId);

            Comment firstArticleComment = articleComments.First();

            firstArticleComment.Id.Should().Be(1);
            firstArticleComment.ArticleId.Should().Be(2);
            firstArticleComment.CommenterName.Should().Be("test user 1");
            firstArticleComment.Text.Should().Be("Article is Awesome");          
        }       
    }
}
