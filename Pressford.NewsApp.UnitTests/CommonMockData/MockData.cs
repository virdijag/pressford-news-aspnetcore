using Pressford.NewsApp.Data.Entities;
using Pressford.NewsApp.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pressford.NewsApp.UnitTests.CommonMockData
{
    public class MockData
    {
        public static ArticleViewModel TestArticleViewModelMock()
        {
            return new ArticleViewModel()
            {
                ArticleComments = new List<CommentViewModel>()
            };
        }
        public static async Task<Article> TestArticleAsyncMock()
        {
            var article = new Article()
            {
                Id = 1,
                Title = "Article Test 1",
                Body = "Article Main Body",
                Author = "Joe Bloggs",
                PublishedDateTime = DateTime.UtcNow,                
            };

            return await Task.FromResult(article);
        }

        public static Article TestArticleMock() => 
            new Article()
            {
                Id = 1,
                Title = "Article Test 1",
                Body = "Article Main Body",
                Author = "Joe Bloggs",
                PublishedDateTime = DateTime.UtcNow,
            };

          
        
        public static Article TestArticle2Mock()
        {
            return new Article()
            {
                Id = 2,
                Title = "Article Test 2",
                Body = "Article Main Body",
                Author = "Joe Bloggs",
                PublishedDateTime = DateTime.UtcNow
            };
        }

        public static async Task<IEnumerable<Article>> TestArticlesMock()
        {
            return new List<Article>()
            {
                await TestArticleAsyncMock(),
                TestArticle2Mock()
            };
        }

        public static Comment TestArticleCommentMock()
        {
            return new Comment()
            {
                Id = 1,
                ArticleId = 2,
                CommenterName = "test user 1",
                Text = "Article is Awesome"
            };
        }

        public static async Task<IEnumerable<Comment>> TestArticleCommentsMock()
        {
            return new List<Comment>()
            {
                await Task.FromResult(TestArticleCommentMock())
            };
        }

        public static async Task<UserProfile> UserProfileMock()
        {
            return new UserProfile()
            {
                UserName = "testuser",
                FirstName = "Joe",
                LastName = "Bloggs",
                RoleId = 1
            };
        }
    }
}
