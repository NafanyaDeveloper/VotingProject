using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTBHackaton.DATA.Repositories;
using VTBHackaton.DATA.ViewModels;

namespace VTBHackaton.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserVariantController : Controller
    {
        private readonly IUserVariantRepository _repo;

        public UserVariantController(IUserVariantRepository repo) => _repo = repo;

        [HttpPost("user")]
        [Produces(typeof(bool))]
        public async Task<ActionResult<bool>> Post([FromBody] UserVariantsViewModel item)
        {
            try
            {
               return Ok(await _repo.AddVariantsToUserAsync(item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPost("Variant")]
        [Produces(typeof(bool))]
        public async Task<ActionResult<bool>> Post([FromBody] VariantUsersViewModel item)
        {
            try
            {
                return Ok(await _repo.AddUsersToVariantAsync(item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpDelete("user")]
        [Produces(typeof(bool))]
        public async Task<ActionResult<bool>> Delete([FromBody] UserVariantsViewModel item)
        {
            try
            {
                return Ok(await _repo.DeleteVariantsFromUserAsync(item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpDelete("Variant")]
        [Produces(typeof(bool))]
        public async Task<ActionResult<bool>> Delete([FromBody] VariantUsersViewModel item)
        {
            try
            {
                return Ok(await _repo.DeleteUsersFromVariantAsync(item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
    }
}
