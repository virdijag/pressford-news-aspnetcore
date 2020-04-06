using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.IdentityProvider;
using Pressford.NewsApp.Repository.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pressford.NewsApp.UnitTests
{
    [TestClass]
    public class UserAccountManagerTests
    {
        private Mock<IUserAccountRepository> userAccountRepositoryMock;
        private IUserAccountManager userAccountManager;

        [TestInitialize]
        public void Init()
        {
            userAccountRepositoryMock = new Mock<IUserAccountRepository>();
            userAccountManager = new UserAccountManager(userAccountRepositoryMock.Object);
        }

        [TestMethod]
        public void UserAccountManager_Constructor_IsNull_ShouldThrow_ArgumentNullException()
        {
            Action action = () => new UserAccountManager(null);
            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task ValidateCredentials_WhenNoUserExists_Expect_UserProfile_IsNull()
        {
            Func<UserProfile> noUserProfile = null;

            userAccountRepositoryMock.Setup(x => x.FindUser(It.IsAny<string>())).ReturnsAsync(noUserProfile);
            
            string userName = "testuser11";
            string password = "testpassword1";
            var user = await userAccountManager.ValidateCredentials(userName, password);

            user.Should().BeNull();
        }

        [TestMethod]
        public async Task ValidateCredentials_WhenUserExists_Expect_UserProfile_IsReturned()
        {
            Task<UserProfile> testUserProfile = TestUserProfile();

            userAccountRepositoryMock.Setup(x => x.FindUser(It.IsAny<string>()))
                                     .Returns(testUserProfile);

            string userName = "testuser1";
            string password = "testpassword1";
            UserProfile user = await userAccountManager.ValidateCredentials(userName, password);

            user.Should().NotBeNull();
        }

        private async Task<UserProfile> TestUserProfile()
        {
            return new UserProfile() {
                Id = 1,
                UserName = "testuser1",
                FirstName = "Joe",
                LastName = "Bloggs",
                RoleId = 1
            };
        }
    }   
}
