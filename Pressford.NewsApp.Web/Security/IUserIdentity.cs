namespace Pressford.NewsApp.Web.Security
{
    public interface IUserIdentity
    {
        string GetName();

        int GetLikeLimit();

        void ReduceLikeLimit();
    }
}
