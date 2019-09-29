using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VTBHackaton.CORE.EF;
using VTBHackaton.DATA.Enteties;

namespace VTBHackaton.API.Requirement
{
    public class RoleHandler: AuthorizationHandler<RoleRequirement>
    {
        private readonly VTBHackatonContext _context;

        public RoleHandler(VTBHackatonContext context) => _context = context;
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
        RoleRequirement requirement)
        {
            var id = context.User
                    .FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = _context.Users.AsNoTracking().FirstOrDefault(a => a.Id == Guid.Parse(id));
                if (user.RoleType == RoleType.Admin)
                {
                        context.Succeed(requirement);
                }
            return Task.CompletedTask;
        }
    }
}
