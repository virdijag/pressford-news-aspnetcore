using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Pressford.NewsApp.Common;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Service.Interfaces;
using Pressford.NewsApp.Web.ViewModels;
using System.Threading.Tasks;

namespace Pressford.NewsApp.Web.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;
        private readonly IMapper mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            this.commentService = commentService.CheckIfNull(nameof(commentService));
            this.mapper = mapper.CheckIfNull(nameof(mapper));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var articleComments = await commentService.GetArticleComments(id);
            return Ok(articleComments);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var mappedEntity = mapper.Map<CommentViewModel, Comment>(model);
               
                await commentService.AddComment(mappedEntity);

                return Ok();
            }

            return BadRequest("Unable to add article comment");
        }
    }
}