using AuthenticationPlugin;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Dynamic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace InventorySystem.Api.Helpers
{
    public static class Security
    {
        public static ExpandoObject decodeToken(HttpRequest Request)
        {
            string fullEncodedToken = Request.Headers["Authorization"];
            string encodedToken = fullEncodedToken.Substring(7); // Remove "Bearer " from token

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(encodedToken);
            var expandoToken = new ExpandoObject();
            foreach (var claim in token.Payload)
            {
                expandoToken.TryAdd(claim.Key, claim.Value);
            }
            return expandoToken;
        }

        public static string HashPassword(string password)
        {
            return SecurePasswordHasherHelper.Hash(password);
        }

        public static bool VerifyHash(string password, string hash)
        {
            return SecurePasswordHasherHelper.Verify(password, hash);
        }

        public static string GetClaimByType(string type, ExpandoObject expandoToken)
        {
            return expandoToken.Where(x => x.Key == type).ToList().FirstOrDefault().Value.ToString();
        }

        // Extended
        public static string GetClaimByType(this ExpandoObject expandoToken, string type)
        {
            return expandoToken.Where(x => x.Key == type).ToList().FirstOrDefault().Value.ToString();
        }
    }
}
