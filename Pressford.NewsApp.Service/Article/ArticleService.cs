using Pressford.NewsApp.Common;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Repository.Interfaces;
using Pressford.NewsApp.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Service
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository articleRepository;

        public ArticleService(IArticleRepository articleRepository) => this.articleRepository = articleRepository.CheckIfNull(nameof(articleRepository));
        
        public async Task CreateArticle(Article article) => await articleRepository.CreateArticle(article);
        
        public async Task<IEnumerable<Article>> GetAllArticles() => await articleRepository.GetAllArticles();

        public async Task<Article> GetArticle(int articleId) => await articleRepository.GetArticle(articleId); 

        public async Task RemoveArticle(int articleId) => await articleRepository.RemoveArticle(articleId);

        public async Task UpdateArticle(Article article) => await articleRepository.UpdateArticle(article);
    }
}
