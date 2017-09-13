// UserService.cs

using CaseTracker.Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CaseTracker.Core.Services
{
    public class HttpContextUserService : IUserService
    {
        private readonly IHttpContextAccessor _context;

        public HttpContextUserService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string CurrentUser => _context.HttpContext.User.Identity.Name;
    }
}