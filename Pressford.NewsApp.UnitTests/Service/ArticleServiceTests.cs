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
    public class ArticleServiceTests
    {
        private Mock<IArticleRepository> articleRepositoryMock;
        private IArticleService articleService;

        [TestInitialize]
        public void Init()
        {
            articleRepositoryMock = new Mock<IArticleRepository>();
            articleService = new ArticleService(articleRepositoryMock.Object);
        }

        [TestMethod]
        public void ArticleService_Constructor_IsNull_Should_Throw_ArgumentNullException()
        {
            Action action = () => new ArticleService(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task CreateArticle_Verify_RepositoryMethod_CreateArticle_IsCalledOnce()
        {
            Article article = await MockData.TestArticleAsyncMock();

            await articleService.CreateArticle(article);

            articleRepositoryMock.Verify(x => x.CreateArticle(It.IsAny<Article>()), Times.Once);
        }

        [TestMethod]
        public async Task GetArticle_Verify_RepositoryMethod_GetArticle_IsCalledOnce()
        {
            int articleId = 1;

            await articleService.GetArticle(articleId);

            articleRepositoryMock.Verify(x => x.GetArticle(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task GetArticle_WhenCalled_Returns_CorrectArticle()
        {
            int articleId = 1;
            articleRepositoryMock.Setup(x => x.GetArticle(articleId)).Returns(MockData.TestArticleAsyncMock());

            Article article = await articleService.GetArticle(articleId);

            article.Id.Should().Be(1);
            article.Title.Should().Be("Article Test 1");
            article.Body.Should().Be("Article Main Body");
            article.Author.Should().Be("Joe Bloggs");
        }

        [TestMethod]
        public async Task UpdateArticle_Verify_RepositoryMethod_UpdateArticle_IsCalledOnce()
        {
            Article article = await MockData.TestArticleAsyncMock();
            article.Body = "Body Updated With new text";

            await articleService.UpdateArticle(article);
            articleRepositoryMock.Verify(x => x.UpdateArticle(It.IsAny<Article>()), Times.Once);
        }

        [TestMethod]
        public void RemoveArticle_Verify_RepositoryMethod_UpdateArticle_IsCalledOnce()
        {
            int articleId = 1;

            articleService.RemoveArticle(articleId);
            articleRepositoryMock.Verify(x => x.RemoveArticle(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task GetAllArticles_Verify_RepositoryMethod_UpdateArticle_IsCalledOnce()
        {
            await articleService.GetAllArticles();

            articleRepositoryMock.Verify(x => x.GetAllArticles(), Times.Once);
        }

        [TestMethod]
        public async Task GetAllArticles_WhenCalled_Returns_AllArticles()
        {
            articleRepositoryMock.Setup(x => x.GetAllArticles()).Returns(MockData.TestArticlesMock());

            IEnumerable<Article> articles = await articleService.GetAllArticles();

            articles.Count().Should().Be(2);

            Article firstArticle = articles.First();

            firstArticle.Id.Should().Be(1);
            firstArticle.Title.Should().Be("Article Test 1");
            firstArticle.Body.Should().Be("Article Main Body");
            firstArticle.Author.Should().Be("Joe Bloggs");

            Article secondArticle = articles.Last();

            secondArticle.Id.Should().Be(2);
            secondArticle.Title.Should().Be("Article Test 2");
            secondArticle.Body.Should().Be("Article Main Body");
            secondArticle.Author.Should().Be("Joe Bloggs");
        }        
    }    
}
