using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TaskManager.Application.Common.Interfaces;

namespace TaskManager.Infrastructure.Security
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public Guid UserId =>
            Guid.Parse(_httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        public string Role =>
            _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.Role)!.Value;
    }
}
