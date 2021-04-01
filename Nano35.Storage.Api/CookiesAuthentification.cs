using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Nano35.Storage.Api
{
    public class CookiesAuthStateProvider : 
        ICustomAuthStateProvider
    {
        private Guid WorkerId { get; set;}

        public Guid CurrentUserId => WorkerId;

        public CookiesAuthStateProvider(
            IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext == null) return;
            var jwtEncoded = GetJwtByHttpContext(httpContextAccessor);
            if (jwtEncoded == "") return;
            WorkerId = Guid.Parse(new JwtSecurityTokenHandler().ReadJwtToken(jwtEncoded).Claims.First().Value);
        }

        private static string GetJwtByHttpContext(IHttpContextAccessor httpContext) => 
            httpContext.HttpContext!.Request.Headers["authorization"]!.ToString().Split(' ').Last();

    }
    public interface ICustomAuthStateProvider
    {
        Guid CurrentUserId {get;}
    }
}