using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pressford.NewsApp.IdentityProvider;
using Pressford.NewsApp.UnitTests.CommonMockData;
using Pressford.NewsApp.Web.Controllers;
using Pressford.NewsApp.Web.ViewModels;
using System;
using System.Threading.Tasks;

namespace Pressford.NewsApp.UnitTests.Web.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        private Mock<IUserAccountManager> accountManagerMock;
        private AccountController accountController;

        [TestInitialize]
        public void Init()
        {
            accountManagerMock = new Mock<IUserAccountManager>();
            accountController = new AccountController(accountManagerMock.Object);
        }

        [TestMethod]
        public void AccountController_Constructor_IsNull_Should_Throw_ArgumentNullException()
        {
            Action action = () => new AccountController(null);

            action.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void Login_When_Called_Should_Return_ViewResult()
        {
            var result = accountController.Login() as ViewResult;
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public async Task Login_When_LoginViewModel_HasErrors_Should_Return_ViewResult()
        {
            accountController.ModelState.AddModelError("", "error");

            var result = await accountController.Login(new LoginViewModel()) as ViewResult;

            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        public async Task Login_When_LoginViewModel_HasNoErrors_WithInValidUser_Should_ViewResult()
        {            
            var result = await accountController.Login(new LoginViewModel()) as ViewResult;

            result.Should().BeOfType<ViewResult>();
        }
        
        [TestMethod]
        public async Task Login_When_LoginViewModel_HasNoErrors_WithValidUser_Should_RedirectToAction_Dashboard()
        {
            accountManagerMock.Setup(x => x.ValidateCredentials(It.IsAny<string>(), It.IsAny<string>())).Returns(MockData.UserProfileMock());
            var result = await accountController.Login(new LoginViewModel()) as RedirectToActionResult;

            result.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Home");
            result.Should().BeOfType<RedirectToActionResult>().Which.ControllerName.Should().Be("App");
        }

        [TestMethod]
        public void LogOut_When_Called_Should_RedirectToAction_Login()
        {
            var result = accountController.LogOut() as RedirectToActionResult;
            result.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Login");
        }       
    }
}
