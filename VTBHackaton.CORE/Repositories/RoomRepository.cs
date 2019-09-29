using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTBHackaton.CORE.EF;
using VTBHackaton.DATA.Converters;
using VTBHackaton.DATA.Dto;
using VTBHackaton.DATA.Repositories;

namespace VTBHackaton.CORE.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly VTBHackatonContext _context;

        public RoomRepository(VTBHackatonContext context) => _context = context;

        public async Task<RoomDto> CreateAsync(RoomDto item)
        {
            try
            {
                if (item == null)
                    return null;
                item.CreationTime = DateTime.Now;
                var res = await _context.Rooms.AddAsync(RoomConverter.Convert(item));
                await _context.SaveChangesAsync();
                return RoomConverter.Convert(res.Entity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                if (id == null)
                    return false;
                var room = await _context.Rooms.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (room == null)
                    return false;
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<RoomDto>> GetAllAsync()
        {
            try
            {
                return await _context.Rooms.AsNoTracking().Select(x => new RoomDto
                {
                    Id = x.Id,
                    Admin = UserConverter.Convert(_context.Users.AsNoTracking().FirstOrDefault(g => g.Id == x.AdminId)),
                    CloseTime = x.CloseTime,
                    CreationTime = x.CreationTime,
                    AdminId = x.AdminId,
                    Description = x.Description,
                    Documents = DocumentConverter.Convert(x.Documents),
                    Polls = x.Polls.Select(z => new PollDto
                    {
                        Id = z.Id,
                        RoomId = z.RoomId,
                        Room = null,
                        Title = z.Title,
                        Variants = z.Variants.Select(a => new VariantDto
                        {
                            Id = a.Id,
                            PollId = a.PollId,
                            Poll = null,
                            Title = a.Title,
                            Users = UserConverter.Convert(a.UserVariant.Select(b => b.User).ToList())
                        }).ToList()
                    }).ToList(),
                    Title = x.Title,
                    Users = UserConverter.Convert(x.UserRoom.Select(y => y.User).ToList())
                }).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<RoomDto> GetById(Guid id)
        {
            try
            {
                return await _context.Rooms.AsNoTracking().Select(x => new RoomDto
                {
                    Id = x.Id,
                    Admin = UserConverter.Convert(_context.Users.AsNoTracking().FirstOrDefault(g => g.Id == x.AdminId)),
                    CloseTime = x.CloseTime,
                    CreationTime = x.CreationTime,
                    AdminId = x.AdminId,
                    Description = x.Description,
                    Messages = MessageConverter.Convert(x.Messages),
                    Documents = DocumentConverter.Convert(x.Documents),
                    Polls = x.Polls.Select(z => new PollDto
                    {
                        Id = z.Id,
                        RoomId = z.RoomId,
                        Room = null,
                        Title = z.Title,
                        Variants = z.Variants.Select(a => new VariantDto
                        {
                            Id = a.Id,
                            PollId = a.PollId,
                            Poll = null,
                            Title = a.Title,
                            Users = UserConverter.Convert(a.UserVariant.Select(b => b.User).ToList())
                        }).ToList()
                    }).ToList(),
                    Title = x.Title,
                    Users = UserConverter.Convert(x.UserRoom.Select(y => y.User).ToList())
                }).FirstOrDefaultAsync(h => h.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(RoomDto item)
        {
            try
            {
                if (item == null || item.Id == null)
                    return false;
                var room = await _context.Rooms.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);
                if (room == null)
                    return false;
                item.CreationTime = room.CreationTime;
                _context.Rooms.Update(RoomConverter.Convert(item));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
