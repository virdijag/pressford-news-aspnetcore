using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pressford.NewsApp.Ef.Core.DataAccess;
using Pressford.NewsApp.Repository;
using Pressford.NewsApp.Repository.Interfaces;
using Pressford.NewsApp.Service.Interfaces;

namespace Pressford.NewsApp.Service
{
    public class RegisterServiceDependencies
    {
        public static void SeedDb(IWebHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<NewsAppSeeder>();
                seeder.Seed();
            }
        }
        public static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<NewsAppContext>(cfg =>
            {
                cfg.UseSqlServer(config.GetConnectionString("NewsAppConnectionString"));
            });
                        
            services.AddTransient<NewsAppSeeder>();
            services.AddTransient<IArticleRepository, ArticleRepository>();
            services.AddTransient<IArticleService, ArticleService>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<ICommentService, CommentService>();
        }
    }
}
