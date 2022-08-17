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
        public string? LanguageName { get; }
        public int? LanguageId { get; }
    }
}
