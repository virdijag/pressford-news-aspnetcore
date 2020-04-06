namespace Pressford.NewsApp.Data.Entities
{
    public class Comment
    {        
        public int Id { get; set; }
              
        public int ArticleId { get; set; }
            
        public string Text { get; set; }
             
        public string CommenterName { get; set; }
    }
}
