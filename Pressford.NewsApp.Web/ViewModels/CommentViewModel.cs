using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Web.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }

        [Required]
        public string Text { get; set; }

        public string CommenterName { get; set; }
    }
}
