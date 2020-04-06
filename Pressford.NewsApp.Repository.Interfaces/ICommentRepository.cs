using Pressford.NewsApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Repository.Interfaces
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetArticleComments(int articleId);
        Task AddComment(Comment comment);
    }
}
