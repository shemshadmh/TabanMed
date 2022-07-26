using System.Security.Claims;
using Application.Interfaces.Application;
using Microsoft.AspNetCore.Http;

namespace TabanMed.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Username => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);
    }
}
