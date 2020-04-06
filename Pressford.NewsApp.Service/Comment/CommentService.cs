using Pressford.NewsApp.Common;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Repository.Interfaces;
using Pressford.NewsApp.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository) => this.commentRepository = commentRepository.CheckIfNull(nameof(commentRepository));
        
        public async Task AddComment(Comment comment) => await commentRepository.AddComment(comment);

        public async Task<IEnumerable<Comment>> GetArticleComments(int articleId) => await commentRepository.GetArticleComments(articleId);
    }
}
