namespace Pressford.NewsApp.Web.Security
{
    public class UserIdentityProvider : IUserIdentity
    {
        private static int limit = 3; //make configurable

        public string GetName() => UserProfileIdentity.UserDetails.Identity.GetName();
        
        public int GetLikeLimit() => limit; 
        public void ReduceLikeLimit() => --limit;        
    }
}
