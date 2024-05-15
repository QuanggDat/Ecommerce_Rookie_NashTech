using System.Security.Claims;

namespace Ecommerce_Rookie_NashTech.Extensions
{
    public static class ClaimExtensions
    {
        public static Guid GetId(this ClaimsPrincipal user)
        {
            var claimType = "UserId";
            var idClaim = user.Claims.FirstOrDefault(x => x.Type == claimType);
            if (idClaim != null)
            {
                return new Guid(idClaim.Value);
            }
            throw new Exception("Invalid token");
        }
    }
}
