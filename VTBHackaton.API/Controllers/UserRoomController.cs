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
    public class UserRoomController: Controller
    {
        private readonly IUserRoomRepository _repo;

        public UserRoomController(IUserRoomRepository repo) => _repo = repo;

        [HttpPost("user")]
        [Produces(typeof(bool))]
        [Authorize(Policy = "RoleLimit")]
        public async Task<ActionResult<bool>> Post([FromBody] UserRoomsViewModel item)
        {
            try
            {
                return Ok(await _repo.AddRoomsToUserAsync(item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPost("room")]
        [Produces(typeof(bool))]
        [Authorize(Policy = "RoleLimit")]
        public async Task<ActionResult<bool>> Post([FromBody] RoomUsersViewModel item)
        {
            try
            {
                return Ok(await _repo.AddUsersToRoomAsync(item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpDelete("user")]
        [Produces(typeof(bool))]
        [Authorize(Policy = "RoleLimit")]
        public async Task<ActionResult<bool>> Delete([FromBody] UserRoomsViewModel item)
        {
            try
            {
                return Ok(await _repo.DeleteRoomsFromUserAsync(item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpDelete("room")]
        [Produces(typeof(bool))]
        [Authorize(Policy = "RoleLimit")]
        public async Task<ActionResult<bool>> Delete([FromBody] RoomUsersViewModel item)
        {
            try
            {
                return Ok(await _repo.DeleteUsersFromRoomAsync(item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
    }
}
