using Pressford.NewsApp.Common;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Ef.Core.DataAccess;
using Pressford.NewsApp.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly NewsAppContext context;

        public ArticleRepository(NewsAppContext context) => 
            this.context = context.CheckIfNull(nameof(context));

        public async Task CreateArticle(Article article)
        {
            await context.AddAsync(article);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Article>> GetAllArticles() => await Task.FromResult(context.Articles.OrderBy(x => x.Id));

        public async Task<Article> GetArticle(int articleId) => await  Task.FromResult(context.Articles.FirstOrDefault(x => x.Id == articleId));

        public async Task RemoveArticle(int articleId)
        {
            Article article = await GetArticle(articleId);
            context.Remove(article);
            await context.SaveChangesAsync();
        }

        public async Task UpdateArticle(Article article)
        {
            Article articleToUpdate = await GetArticle(article.Id);

            articleToUpdate.Title = article.Title;
            articleToUpdate.Body = article.Body;
            articleToUpdate.Author = article.Author;
            articleToUpdate.NumberOfLikes = article.NumberOfLikes;

            context.Update(articleToUpdate);
            await context.SaveChangesAsync();
        }
    }
}
