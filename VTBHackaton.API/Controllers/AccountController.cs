using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VTBHackaton.CORE.EF;
using VTBHackaton.DATA.Dto;
using VTBHackaton.DATA.Repositories;

namespace VTBHackaton.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly VTBHackatonContext _context;
        private readonly IUserRepository _repo;

        public AccountController(VTBHackatonContext context, IUserRepository repo)
        {
            _repo = repo;
            _context = context;
        }

        [HttpGet]
        [Produces(typeof(UserDto))]
        public async Task<ActionResult<UserDto>> Info()
        {
            try
            {
                var id = HttpContext.User
                    .FindFirst(ClaimTypes.NameIdentifier).Value;
                return await _repo.GetByIdAsync(Guid.Parse(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
       

    }
}
