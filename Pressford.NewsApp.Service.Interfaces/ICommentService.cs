using Pressford.NewsApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Service.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetArticleComments(int articleId);
        Task AddComment(Comment comment);
    }
}
