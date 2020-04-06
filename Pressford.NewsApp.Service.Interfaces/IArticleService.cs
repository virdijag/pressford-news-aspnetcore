using Pressford.NewsApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Service.Interfaces
{
    public interface IArticleService
    {
        Task CreateArticle(Article article);

        Task<Article> GetArticle(int articleId);

        Task<IEnumerable<Article>> GetAllArticles();

        Task UpdateArticle(Article article);

        Task RemoveArticle(int articleId);
    }
}
