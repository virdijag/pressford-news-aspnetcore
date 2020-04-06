using Pressford.NewsApp.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Pressford.NewsApp.Data.Entities;

namespace Pressford.NewsApp.Ef.Core.DataAccess
{
    public class NewsAppSeeder
    {
        private readonly NewsAppContext context;

        public NewsAppSeeder(NewsAppContext context)
        {
            this.context = context.CheckIfNull(nameof(context));
        }
        public void Seed()
        {
            context.Database.EnsureCreated();

            if (!context.Articles.Any())
            {
                context.Articles.Add(Article1());
                context.Articles.Add(Article2());
                context.Articles.Add(Article3());
                context.Articles.Add(Article4());
                context.SaveChanges();

                context.Comments.Add(Comment1());
                context.Comments.Add(Comment2());
                context.Comments.Add(Comment3());
                context.Comments.Add(Comment4());
                context.SaveChanges();
            }
        }

        private Comment Comment1()
        {
            return new Comment() { ArticleId = 1, CommenterName = "Rohan Penta", Text = "This is awesome!!" };
        }
        private Comment Comment2()
        {
            return new Comment() {  ArticleId = 1, CommenterName = "Joe Blogss", Text = "Thank you Rohan" };
        }
        private Comment Comment3()
        {
            return new Comment() {  ArticleId = 1, CommenterName = "Rohan Penta", Text = "Cant wait for the new article" };
        }
        private Comment Comment4()
        {
            return new Comment() { ArticleId = 2, CommenterName = "Joe Blogs", Text = "Great Article!!" };
        }
        private Article Article1()
        {
            return new Article()
            {
                Author = "Joe Bloggs",
                Title = "New Opportunities at Pressford Consulting",
                Body = ArticleBodySamples.Article1,
                PublishedDateTime = DateTime.UtcNow,
                NumberOfLikes = 6
            };
        }
        private Article Article2()
        {
            return new Article()
            {
                Author = "Joe Bloggs",
                Title = "Pressford Consulting Huge Investment in Technology",
                Body = ArticleBodySamples.Article2,
                PublishedDateTime = DateTime.UtcNow,
                NumberOfLikes = 2
            };
        }

        private Article Article3()
        {
            return new Article()
            {
                Author = "Rohan Penta",
                Title = "Reducing Our Carbon Foot Print",
                Body = ArticleBodySamples.Article3,
                PublishedDateTime = DateTime.UtcNow,
                NumberOfLikes = 8
            };
        }

        private Article Article4()
        {
            return new Article()
            {
                Author = "Joe Bloggs",
                Title = "Pressford Consulting Win Best Place To Work 2019",
                Body = ArticleBodySamples.Article4,
                PublishedDateTime = DateTime.UtcNow,
                NumberOfLikes = 10
            };
        }
    }
}
