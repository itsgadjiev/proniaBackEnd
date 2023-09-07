using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _appDbContext;
        private User _user;
        public UserService(IHttpContextAccessor httpContextAccessor,AppDbContext appDbContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _appDbContext = appDbContext;
        }

        public bool IsCurrentUserAuthenticated()
        {
            return _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public User GetCurrentUser()
        {
            if (!IsUserAuthorized())
            {
               
            }
            return;

        }

    }
}
