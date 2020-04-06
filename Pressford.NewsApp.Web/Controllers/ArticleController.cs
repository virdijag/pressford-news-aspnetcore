using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pressford.NewsApp.Common;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Service.Interfaces;
using Pressford.NewsApp.Web.Security;
using Pressford.NewsApp.Web.ViewModels;
using System;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Web.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService articleService;
        private readonly IMapper mapper;
        private readonly IUserIdentity userIdentity;

        public ArticleController(
            IArticleService articleService,
            IMapper mapper,
            IUserIdentity userIdentity)
        {
            this.articleService = articleService.CheckIfNull(nameof(articleService));
            this.mapper = mapper.CheckIfNull(nameof(mapper));
            this.userIdentity = userIdentity.CheckIfNull(nameof(userIdentity));
        }

        private string userProfileName => userIdentity.GetName();

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var articles = await articleService.GetAllArticles();
            return Ok(articles);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var article = await articleService.GetArticle(id);
            return Ok(article);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedEntity = mapper.Map<ArticleViewModel, Article>(model);
                mappedEntity.Author = userProfileName;
                mappedEntity.NumberOfLikes = 0;
                mappedEntity.PublishedDateTime = DateTime.UtcNow;

                await articleService.CreateArticle(mappedEntity);

                return Ok();
            }

            return BadRequest("Invalid Article");
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedEntity = mapper.Map<ArticleViewModel, Article>(model);
                mappedEntity.Author = userProfileName;
                mappedEntity.PublishedDateTime = DateTime.UtcNow;

                await articleService.UpdateArticle(mappedEntity);

                return Ok();
            }

            return BadRequest("Invalid Article");
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await articleService.RemoveArticle(id);

            return Ok();
        }
    }
}