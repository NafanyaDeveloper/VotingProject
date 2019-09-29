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
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly IDocumentRepository _repo;
        private readonly VTBHackatonContext _context;

        public DocumentController(IDocumentRepository repo, VTBHackatonContext context)
        {
            _context = context;
            _repo = repo;
        }

        [HttpGet]
        [Authorize(Policy = "RoleLimit")]
        [Produces(typeof(List<DocumentDto>))]
        public async Task<ActionResult<List<DocumentDto>>> Get()
        {
            try
            {
               /* var id = HttpContext.User
                    .FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = */
                return Ok(await _repo.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpGet("{id}")]
        [Produces(typeof(DocumentDto))]
        public async Task<ActionResult<DocumentDto>> Get(Guid id)
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
        [Authorize(Policy = "RoleLimit")]
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
        [Produces(typeof(DocumentDto))]
        [Authorize(Policy = "RoleLimit")]
        public async Task<ActionResult<DocumentDto>> Post([FromBody] DocumentDto item)
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
        public async Task<ActionResult<bool>> Put([FromBody] DocumentDto item)
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
