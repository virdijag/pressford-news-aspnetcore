using Pressford.NewsApp.Common;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Repository.Interfaces;
using System.Threading.Tasks;

namespace Pressford.NewsApp.IdentityProvider
{
    public sealed class UserAccountManager : IUserAccountManager
    {
        private readonly IUserAccountRepository userAccountRepository;

        public UserAccountManager(IUserAccountRepository userAccountRepository) => 
            this.userAccountRepository = userAccountRepository.CheckIfNull(nameof(userAccountRepository));
        
        public async Task<UserProfile> ValidateCredentials(string userName, string password) => await userAccountRepository.FindUser(userName);        
    }
}
