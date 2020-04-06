using Pressford.NewsApp.Common;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Ef.Core.DataAccess;
using Pressford.NewsApp.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly NewsAppContext context;

        public CommentRepository(NewsAppContext context) =>  this.context = context.CheckIfNull(nameof(context));
        public async Task AddComment(Comment comment)
        {
            await context.AddAsync(comment);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetArticleComments(int articleId) => await Task.FromResult(context.Comments.Where(x => x.ArticleId == articleId));        
    }
}
