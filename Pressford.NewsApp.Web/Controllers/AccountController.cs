using Microsoft.AspNetCore.Mvc;
using Pressford.NewsApp.Common;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.IdentityProvider;
using Pressford.NewsApp.Web.Security;
using Pressford.NewsApp.Web.ViewModels;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAccountManager userAccountManager;
    
        public AccountController(IUserAccountManager userAccountManager) => 
            this.userAccountManager = userAccountManager.CheckIfNull(nameof(userAccountManager));      
        
        [HttpGet]
        public IActionResult Login() => View();
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfile user = await userAccountManager.ValidateCredentials(model.UserName, model.Password);

                if (user != null)
                {
                    UserProfileIdentity.SignIn(user);
                    return RedirectToAction("Home", "App");
                }
            }

            ModelState.AddModelError("", "Incorrect username or password");
            return View();                     
        }
        public IActionResult LogOut()
        {
            UserProfileIdentity.SignOut();
            return RedirectToAction("Login");
        }
    }
}