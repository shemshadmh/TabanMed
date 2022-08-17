using System.Security.Claims;
using Application.Interfaces.Application;
using Microsoft.AspNetCore.Http;

namespace TabanMed.Infrastructure.Services
{
    public class CurrentServices : ICurrentServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Username => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
        public string LanguageDisplayName => Thread.CurrentThread.CurrentCulture.DisplayName;
        public int LanguageId => Thread.CurrentThread.CurrentCulture.LCID;
    }
}
