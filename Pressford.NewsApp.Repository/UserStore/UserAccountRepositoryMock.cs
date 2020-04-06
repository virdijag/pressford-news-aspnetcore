using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Repository
{
    public sealed class UserAccountRepositoryMock : IUserAccountRepository
    {
        private IEnumerable<UserProfile> userProfiles;

        public UserAccountRepositoryMock()
        {
            userProfiles = new List<UserProfile>()
            {
                new UserProfile() { Id = 1, UserName = "testuser1", FirstName = "Joe", LastName = "Bloggs", RoleId = 1 }, // Publisher
                new UserProfile() { Id = 2, UserName = "testuser2", FirstName = "Pete", LastName = "Briggs", RoleId = 2 }, // Employee
                new UserProfile() { Id = 3, UserName = "testuser3", FirstName = "Rohan", LastName = "Penta", RoleId = 2 }, // Employee
            };
        }
        public async Task<UserProfile> FindUser(string userName) => await Task.FromResult(userProfiles.FirstOrDefault(x => x.UserName == userName));
    }
}
