using Pressford.NewsApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Web.ViewModels
{
    public class HomeViewModel
    {
        public ICollection<Article> Articles { get; set; }
    }
}
