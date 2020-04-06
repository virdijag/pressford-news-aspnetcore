using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.IdentityProvider;
using System;
using FluentAssertions;
using Pressford.NewsApp.Repository;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Integration.Tests
{
    [TestClass]
    public class UserAccountManagerTests
    {
        private IUserAccountManager userAccountManager;

        [TestInitialize]
        public void Init() => userAccountManager = new UserAccountManager(new UserAccountRepositoryMock());
        
        [TestMethod]
        public void UserAccountManager_Constructor_IsNull_ShouldThrow_ArgumentNullException()
        {
            Action action = () => new UserAccountManager(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task ValidateCredentials_WhenNoUserExists_Expect_UserProfile_IsNull()
        {         
            string userName = "notestuser1";
            string password = "testpassword1";
            UserProfile user = await userAccountManager.ValidateCredentials(userName, password);

            user.Should().BeNull();
        }

        [TestMethod]
        public async Task ValidateCredentials_WhenUserExists_Expect_UserProfile_IsReturned()
        {            
            string userName = "testuser1";
            string password = "testpassword1";
            UserProfile user = await userAccountManager.ValidateCredentials(userName, password);

            user.Should().NotBeNull();
        }
    }
}
