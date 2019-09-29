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
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly VTBHackatonContext _context;

        public ChatController(IHubContext<ChatHub> hc, VTBHackatonContext context)
        {
            _hubContext = hc;
            _context = context;
        }

        [Authorize]
        [Produces(typeof(bool))]
        [HttpGet]
        public async Task<ActionResult<bool>> Send([FromBody] MessageDto message)
        {
            try
            {
                
                string id = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                User user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
                if (user == null)
                    return BadRequest();
                message.UserId = Guid.Parse(id);
                var room = await _context.Rooms.AsNoTracking().Select(y => new
                {
                    Id = y.Id,
                    UsersId = y.UserRoom.Select(z => z.UserId.ToString()).ToList()
                }).FirstOrDefaultAsync(x => x.Id == message.RoomId);
                if (room == null)
                    return BadRequest();
                message.Date = DateTime.Now;
                await _hubContext.Clients.Users(room.UsersId.AsReadOnly()).SendAsync(
                    "Send", message);
                await _context.Messages.AddAsync(MessageConverter.Convert(message));
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
