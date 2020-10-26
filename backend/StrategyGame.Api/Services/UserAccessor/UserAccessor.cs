using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StrategyGame.Api.Services.UserAccessor
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var subClaim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "sub");
                if (subClaim == null)
                {
                    throw new InvalidOperationException("Sub claim is missing. Is the caller authenticated?");
                }
                if (int.TryParse(subClaim.Value, out int id))
                {
                    return id;
                }
                else
                {
                    throw new InvalidOperationException($"Sub claim is invalid. Was {subClaim.Value}, should be an integer.");
                }
            }
        }
    }
}
