using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pressford.NewsApp.Common;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Service.Interfaces;
using Pressford.NewsApp.Web.Security;
using Pressford.NewsApp.Web.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Web.Controllers
{
    // [Authorize(Roles = "Publisher")]
    public class AdminController : Controller
    {
        private readonly IArticleService articleService;
        private readonly IMapper mapper;
        private readonly IUserIdentity userIdentity;

        public AdminController(
            IArticleService articleService,
            IMapper mapper,
            IUserIdentity userIdentity)
        {
            this.articleService = articleService.CheckIfNull(nameof(articleService));
            this.mapper = mapper.CheckIfNull(nameof(mapper));
            this.userIdentity = userIdentity.CheckIfNull(nameof(userIdentity));
        }
        private string userProfileName => userIdentity.GetName();
        public async Task<IActionResult> Dashboard()
        {
            var articles = await articleService.GetAllArticles();

            return View(new DashboardViewModel()
            {
                Articles = articles.ToList()
            });
        }
        public IActionResult CreateArticle() => View(new ArticleViewModel());
        
        [HttpPost]
        public async Task<IActionResult> CreateArticle(ArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var mappedEntity = mapper.Map<ArticleViewModel, Article>(model);
            mappedEntity.Author = userProfileName;
            mappedEntity.NumberOfLikes = 0;
            mappedEntity.PublishedDateTime = DateTime.UtcNow;

            await articleService.CreateArticle(mappedEntity);

            var articles = await articleService.GetAllArticles();

            return View("Dashboard", new DashboardViewModel()
            {
                Articles = articles.ToList()
            });
        }

        public async Task<IActionResult> EditArticle(int id)
        {
            var article = await articleService.GetArticle(id);

            var mappedEntity = mapper.Map<Article, ArticleViewModel>(article);
            return View(mappedEntity);
        }

        [HttpPost]
        public async Task<IActionResult> EditArticle(ArticleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var mappedEntity = mapper.Map<ArticleViewModel, Article>(model);
            mappedEntity.Author = userProfileName;
            mappedEntity.PublishedDateTime = DateTime.UtcNow;

            await articleService.UpdateArticle(mappedEntity);

            return RedirectToAction("Dashboard");
        }

        public async Task<IActionResult> DeleteArticle(int id)
        {
            await articleService.RemoveArticle(id);

            return RedirectToAction("Dashboard");
        }
    }
}