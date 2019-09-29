using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VTBHackaton.DATA.Dto;
using VTBHackaton.DATA.Repositories;

namespace VTBHackaton.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class RoomController : Controller
    {
        private readonly IRoomRepository _repo;

        public RoomController(IRoomRepository repo) => _repo = repo;

        [HttpGet]
        [Produces(typeof(List<RoomDto>))]
        [Authorize(Policy = "RoleLimit")]
        public async Task<ActionResult<List<RoomDto>>> Get()
        {
            try
            {
                return Ok(await _repo.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpGet("{id}")]
        [Produces(typeof(RoomDto))]
        public async Task<ActionResult<RoomDto>> Get(Guid id)
        {
            try
            {
                var res = await _repo.GetById(id);
                if (res == null)
                    return BadRequest();
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpDelete("{id}")]
        [Produces(typeof(bool))]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                return Ok(await _repo.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPost]
        [Produces(typeof(RoomDto))]
        [Authorize(Policy = "RoleLimit")]
        public async Task<ActionResult<RoomDto>> Post([FromBody] RoomDto item)
        {
            try
            {
                var res = await _repo.CreateAsync(item);
                if (res == null)
                    return BadRequest();
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPut]
        [Produces(typeof(bool))]
        [Authorize(Policy = "RoleLimit")]
        public async Task<ActionResult<bool>> Put([FromBody] RoomDto item)
        {
            try
            {
                return Ok(await _repo.UpdateAsync(item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

    }
}
