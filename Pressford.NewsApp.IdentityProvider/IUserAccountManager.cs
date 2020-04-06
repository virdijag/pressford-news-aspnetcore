using Pressford.NewsApp.Data.Entities;
using System.Threading.Tasks;

namespace Pressford.NewsApp.IdentityProvider
{
    public interface IUserAccountManager
    {
        Task<UserProfile> ValidateCredentials(string userName, string password);
    }
}
