using AutoMapper;
using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Web.ViewModels;

namespace Pressford.NewsApp.Web.Mappers
{
    public class NewsAppMappingProfile : Profile
    {
        public NewsAppMappingProfile()
        {
            CreateMap<Article, ArticleViewModel>()
            .ReverseMap();

            CreateMap<Comment, CommentViewModel>()
           .ReverseMap();
        }
    }
}
