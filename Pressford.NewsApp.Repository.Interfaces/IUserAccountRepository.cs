using Pressford.NewsApp.Data.Entities;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Repository.Interfaces
{
    public interface IUserAccountRepository
    {
        Task<UserProfile> FindUser(string userName);
    }
}
