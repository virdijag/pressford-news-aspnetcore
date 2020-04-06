using Microsoft.EntityFrameworkCore;
using Pressford.NewsApp.Data.Entities;
using System;

namespace Pressford.NewsApp.Ef.Core.DataAccess
{
    public class NewsAppContext : DbContext
    {
        public NewsAppContext(DbContextOptions<NewsAppContext> options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }

        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=NewsAppDB;Integrated Security=true;");
            }
        }       
    }
}
