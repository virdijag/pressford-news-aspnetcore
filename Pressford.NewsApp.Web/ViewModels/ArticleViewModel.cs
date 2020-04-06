using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Web.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        public string Author { get; set; }

        public int NumberOfLikes { get; set; }

        public DateTime PublishedDateTime { get; set; }

        public IEnumerable<CommentViewModel> ArticleComments { get; set; }

        public CommentViewModel Comment { get; set; }

        public bool  LikeLimitReached { get; set; }
    }
}
