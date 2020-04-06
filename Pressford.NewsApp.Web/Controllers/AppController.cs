using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pressford.NewsApp.Common;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Service.Interfaces;
using Pressford.NewsApp.Web.Security;
using Pressford.NewsApp.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Web.Controllers
{
    public class AppController : Controller
    {
        private readonly IArticleService articleService;
        private readonly ICommentService commentService;
        private readonly IMapper mapper;
        private readonly IUserIdentity userIdentity;

        public AppController(
            IArticleService articleService,
            ICommentService commentService,
            IMapper mapper,
            IUserIdentity userIdentity)
        {
            this.articleService = articleService.CheckIfNull(nameof(articleService));
            this.commentService = commentService.CheckIfNull(nameof(commentService));
            this.mapper = mapper.CheckIfNull(nameof(mapper));
            this.userIdentity = userIdentity.CheckIfNull(nameof(userIdentity));
        }
       
        public async Task<IActionResult> Home()
        {          
            var articles = await articleService.GetAllArticles();

            return View(new HomeViewModel()
            {
                Articles = articles.ToList()
            });
        }

        public async Task<IActionResult> Article(int id)
        {
            var article = await articleService.GetArticle(id);
            var articlesMapped = mapper.Map<Article, ArticleViewModel>(article);

            var comments = await commentService.GetArticleComments(id);
            List<CommentViewModel> commentMapped = mapper.Map<List<Comment>, List<CommentViewModel>>(comments.ToList());
            articlesMapped.ArticleComments = commentMapped;
            return View(articlesMapped);           
        }
               
        [HttpPost]
        public async Task<IActionResult> AddArticleLike(ArticleViewModel model)
        {
            int limit = userIdentity.GetLikeLimit();

            if (limit == 0)
            {
                model.LikeLimitReached = true;
                return View("Article", model);
            }

            userIdentity.ReduceLikeLimit();

            model.NumberOfLikes++;

            var mappedEntity = mapper.Map<ArticleViewModel, Article>(model);

            await articleService.UpdateArticle(mappedEntity);

            return RedirectToAction("Article", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(ArticleViewModel articleComment)
        {
            var mappedEntity = mapper.Map<CommentViewModel, Comment>(articleComment.Comment);
            mappedEntity.ArticleId = articleComment.Id;
            mappedEntity.CommenterName = userIdentity.GetName();
            await commentService.AddComment(mappedEntity);
         
            return RedirectToAction("Article", new { id = articleComment.Id });
        }
    }
}