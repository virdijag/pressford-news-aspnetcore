using System;

namespace Pressford.NewsApp.Common
{
    public static class NullGuarder
    {
        public static T CheckIfNull<T>(this T argument, string message) where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(message);
            }

            return argument;
        }
    }
}
