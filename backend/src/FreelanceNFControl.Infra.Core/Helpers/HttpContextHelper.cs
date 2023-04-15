using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace FreelanceNFControl.Infra.Core.Helpers
{
    public class HttpContextHelper : IHttpContextHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            var decodedToken = JwtHelper.DecryptToken(GetTokenFromRequest());

            if (!decodedToken.Claims.Any(c => c.Type == "nameid"))
                throw new UnauthorizedAccessException("Invalid Token");

            return decodedToken.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
        }

        private string GetTokenFromRequest()
        {
            var headerToken = _httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString();
            var bearerToken = headerToken.Replace("Bearer", string.Empty).Trim();
            return bearerToken;
        }
    }
}
