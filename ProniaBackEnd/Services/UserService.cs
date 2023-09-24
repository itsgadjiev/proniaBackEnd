using ProniaBackEnd.Database;
using ProniaBackEnd.Database.Models;

namespace ProniaBackEnd.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AppDbContext _appDbContext;
        private User _user;
        public UserService(IHttpContextAccessor httpContextAccessor, AppDbContext appDbContext)
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
            if (_user != null)
            {
                return _user;
            }

            if (!IsCurrentUserAuthenticated())
            {
                throw new Exception("User is not authenticated");
            }

            var userClaimId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userClaimId is null)
            {
                throw new Exception("User is not authenticated");
            }

            var userClaimIdInt = Convert.ToInt32(userClaimId.Value);
            var user = _appDbContext.Users.SingleOrDefault(x => x.Id == userClaimIdInt);

            if (user is null)
            {
                throw new Exception("User is not found");
            }

            _user = user;

            return _user;

        }

        public string GetCurrentUserFullName()
        {
            User user = GetCurrentUser();
            return $"{user.Name} {user.LastName}";
        }

        public List<User> GetAllStaffMembers()
        {
            return _appDbContext.Users
                .Where(x => x.Role == Role.RoleEnums.Admin
                || x.Role == Role.RoleEnums.SuperAdmin
                || x.Role == Role.RoleEnums.Moderator)
                .ToList();
        }


        public List<User> GetAllBasicMembers()
        {
            return _appDbContext.Users
                .Where(x => x.Role == Role.RoleEnums.User)
                .ToList();
        }
    }
}
