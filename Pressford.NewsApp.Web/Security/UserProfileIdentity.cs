using Pressford.NewsApp.Data.Entities;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace Pressford.NewsApp.Web.Security
{
    public static class UserProfileIdentity
    {
        public static ClaimsPrincipal UserDetails { get; private set; }
        public static void SignOut() => UserDetails = null;
        public static void SignIn(UserProfile user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(GetUserClaims(user), "Custom");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            UserDetails = principal;
        }

        public static string GetName(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Name);

            return claim?.Value ?? string.Empty;
        }
        
        public static string GetUserName(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            return claim?.Value ?? string.Empty;
        }

        public static bool IsPublisher(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst(ClaimTypes.Role);

            var roleValue =  claim?.Value ?? string.Empty;
            
            return roleValue == UserRole.Publisher.ToString();
        }
        private static IEnumerable<Claim> GetUserClaims(UserProfile user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
            claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));
            claims.AddRange(GetUserRoleClaims(user));
            return claims;
        }

        private static IEnumerable<Claim> GetUserRoleClaims(UserProfile user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserName));
            claims.Add(new Claim(ClaimTypes.Role, GetRole(user.RoleId)));
            return claims;
        }

        private static string GetRole(int roleId)
        {
            int publisherRoleId = 1;

            if (roleId == publisherRoleId)
            {
                return UserRole.Publisher.ToString();
            }

            return UserRole.Employee.ToString();
        }
    }
}
