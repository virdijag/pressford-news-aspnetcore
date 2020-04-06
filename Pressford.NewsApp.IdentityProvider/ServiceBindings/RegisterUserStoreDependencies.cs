using Microsoft.Extensions.DependencyInjection;
using Pressford.NewsApp.Repository;
using Pressford.NewsApp.Repository.Interfaces;

namespace Pressford.NewsApp.IdentityProvider
{
    public class RegisterUserStoreDependencies
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IUserAccountRepository, UserAccountRepositoryMock>();
            services.AddTransient<IUserAccountManager, UserAccountManager>();
        }
    }
}
