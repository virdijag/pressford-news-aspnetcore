using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Ef.Core.DataAccess;
using Pressford.NewsApp.Integration.Tests.Repository;
using Pressford.NewsApp.Repository;
using Pressford.NewsApp.Repository.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Integration.Tests
{
    [TestClass]
    public class CommentRepositoryTests : BaseRepository
    {
        private NewsAppContext context;
        private ICommentRepository commentRepository;

        [TestInitialize]
        public void Init()
        {
            InitialiseDbContext(context);
            commentRepository = new CommentRepository(context);
        }

        [TestMethod]
        public async Task GetArticleComments_By_ArticleId_Expect_CorrectArticleComments_Returned()
        {
            int articleId = 1;
            var comments = await commentRepository.GetArticleComments(articleId);

            comments.Count().Should().BeGreaterThan(0);
        }

        [TestMethod]
        public async Task AddComment_Expect_CommentIsAdded()
        {
            DateTime publishedDateTime = DateTime.UtcNow;
            Comment comment = new Comment()
            {
               ArticleId = 6,
               CommenterName = "Joe Bloggs",
               Text = "test comments"
            };

            await commentRepository.AddComment(comment);

            int articleId = 6;
            var result = await commentRepository.GetArticleComments(articleId);

            var comments = result.ToList();

            comments[0].ArticleId.Should().Be(6);
            comments[0].CommenterName.Should().Be("Joe Bloggs");
            comments[0].Text.Should().Be("test comments");
        }

        [TestCleanup]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }

    }
}
