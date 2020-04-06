using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pressford.NewsApp.Ef.Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pressford.NewsApp.Integration.Tests.Repository
{
    public class BaseRepository
    {
        protected void InitialiseDbContext(NewsAppContext context)
        {
            var serviceProvider = new ServiceCollection()
              .AddEntityFrameworkSqlServer()
              .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<NewsAppContext>();

            builder.UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=NewsAppDB;Integrated Security=true;")
                   .UseInternalServiceProvider(serviceProvider);

            context = new NewsAppContext(builder.Options);
            context.Database.Migrate();
            NewsAppSeeder seeder = new NewsAppSeeder(context);
            seeder.Seed();
        }
    }
}
