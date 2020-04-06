using System;

namespace Pressford.NewsApp.Data.Entities
{
    public class Article
    {
        public int Id {  get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public string Author { get; set; }

        public int NumberOfLikes { get; set; }

        public DateTime PublishedDateTime { get; set; }
    }
}
