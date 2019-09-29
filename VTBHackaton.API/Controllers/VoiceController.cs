using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VTBHackaton.CORE.EF;
using VTBHackaton.CORE.Hubs;
using VTBHackaton.DATA.Converters;
using VTBHackaton.DATA.Dto;
using VTBHackaton.DATA.Enteties;

namespace VTBHackaton.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class VoiceController : Controller
    {
        private readonly IHubContext<VoiceHub> _hubContext;
        private readonly VTBHackatonContext _context;

        public VoiceController(IHubContext<VoiceHub> hc, VTBHackatonContext context)
        {
            _hubContext = hc;
            _context = context;
        }

        [Authorize]
        [Produces(typeof(bool))]
        [HttpGet("{id}")]
        public async Task<ActionResult<bool>> Send(Guid variantId)
        {
            try
            {

                string id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                User user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
                if (user == null)
                    return BadRequest();
                var variant = await _context.Variants.AsNoTracking().Select(a => new
                {
                    Id = a.Id,
                    roomId = a.Poll.RoomId
                }).FirstOrDefaultAsync(y => y.Id == variantId);
                if (variant == null)
                    return BadRequest();
                var room = await _context.Rooms.AsNoTracking().Select(z => new
                {
                    usersId = z.UserRoom.Select(f => f.UserId.ToString()).ToList(),
                    Id = z.Id
                }).FirstOrDefaultAsync(y => y.Id == variant.roomId);
        
                if (room == null)
                    return BadRequest();
                UserVariant uv = new UserVariant { UserId = Guid.Parse(id), VariantId = variantId };
                await _hubContext.Clients.Users(room.usersId.AsReadOnly()).SendAsync(
                    "Send", uv);
                await _context.UserVariant.AddAsync(uv);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
