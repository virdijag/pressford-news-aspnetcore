using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Ef.Core.DataAccess;
using Pressford.NewsApp.Integration.Tests.Repository;
using Pressford.NewsApp.Repository;
using Pressford.NewsApp.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Integration.Tests
{
    [TestClass]
    public class ArticleRepositoryTests : BaseRepository
    {
        private NewsAppContext context;
        private IArticleRepository articleRepository;

        [TestInitialize]
        public void Init()
        {
            InitialiseDbContext(context);
            articleRepository = new ArticleRepository(context);
        }

        [TestMethod]
        public async Task GetArticle_By_ArticleId_Expect_CorrectArticle_Returned()
        {
            int articleId = 1;
            var article = await articleRepository.GetArticle(articleId);

            article.Id.Should().Be(1);
            article.Title.Should().Be("New Opportunities at Pressford Consulting");
        }

        [TestMethod]
        public async Task CreateArticle_Expect_ArticleIsCreated()
        {
            DateTime publishedDateTime = DateTime.UtcNow;
            Article article = new Article()
            {
                Title = "New Article",
                Author = "test user",
                NumberOfLikes = 3,
                PublishedDateTime = publishedDateTime
            };

            await articleRepository.CreateArticle(article);

            int articleId = 5;

            var newlyAddedArticle = await articleRepository.GetArticle(articleId);

            newlyAddedArticle.Id.Should().Be(5);
            newlyAddedArticle.Title.Should().Be("New Article");
            newlyAddedArticle.PublishedDateTime.Should().Be(article.PublishedDateTime);
        }

        [TestMethod]
        public async Task UpdateArticle_Expect_ArticleIsUpdated()
        {
            DateTime publishedDateTime = DateTime.UtcNow;

            Article article = new Article()
            {
                Id = 1,
                Title = "Updated Article",
                Author = "test user",
                NumberOfLikes = 15,
                PublishedDateTime = publishedDateTime
            };

            await articleRepository.UpdateArticle(article);

            int articleId = 1;
            var updatedAddedArticle = await articleRepository.GetArticle(articleId);
            updatedAddedArticle.Id.Should().Be(1);
            updatedAddedArticle.Title.Should().Be("Updated Article");
            updatedAddedArticle.NumberOfLikes.Should().Be(15);
        }

        [TestCleanup]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }
    }
}
