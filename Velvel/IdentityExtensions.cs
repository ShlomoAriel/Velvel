//using System.Security.Claims;
//using System.Security.Principal;

//namespace Velvel
//{
//    public static class IdentityExtensions
//    {
//        public static string GetUserId(this IIdentity identity)
//        {
//            var claim = ((ClaimsIdentity)identity).FindFirst("UserId");
//            // Test for null to avoid issues during local testing
//            return (claim != null) ? claim.Value : string.Empty;
//        }
//    }
//}