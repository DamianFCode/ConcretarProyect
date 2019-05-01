using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace Concretar.BackEnd.Authorization
{
    public class UserAccessAuthorization : IAuthorizationRequirement
    {
        public UserAccessAuthorization(IConfiguration configuration)
        {
        }
    }
}
