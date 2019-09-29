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
    public class PollRepository: IPollRepository
    {
        private readonly VTBHackatonContext _context;

        public PollRepository(VTBHackatonContext context) => _context = context;

        public async Task<PollDto> CreateAsync(PollDto item)
        {
            try
            {
                if (item == null)
                    return null;
                var res = await _context.Polls.AddAsync(PollConverter.Convert(item));
                await _context.SaveChangesAsync();
                return PollConverter.Convert(res.Entity);
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
                var Poll = await _context.Polls.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (Poll == null)
                    return false;
                _context.Polls.Remove(Poll);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<PollDto>> GetAllAsync()
        {
            try
            {
                return await _context.Polls.AsNoTracking().Select(z => new PollDto
                {
                    Id = z.Id,
                    Title = z.Title,
                    RoomId = z.RoomId,
                    Variants = z.Variants.Select(a => new VariantDto
                    {
                        Id = a.Id,
                        PollId = a.PollId,
                        Poll = null,
                        Title = a.Title,
                        Users = UserConverter.Convert(a.UserVariant.Select(b => b.User).ToList())
                    }).ToList(),
                    Room = RoomConverter.Convert(z.Room)
                }).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<PollDto> GetById(Guid id)
        {
            try
            {
                return await _context.Polls.AsNoTracking().Select(z => new PollDto
                {
                    Id = z.Id,
                    Title = z.Title,
                    RoomId = z.RoomId,
                    Variants = z.Variants.Select(a => new VariantDto
                    {
                        Id = a.Id,
                        PollId = a.PollId,
                        Poll = null,
                        Title = a.Title,
                        Users = UserConverter.Convert(a.UserVariant.Select(b => b.User).ToList())
                    }).ToList(),
                    Room = RoomConverter.Convert(z.Room)
                }).FirstOrDefaultAsync(h => h.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(PollDto item)
        {
            try
            {
                if (item == null || item.Id == null)
                    return false;
                var Poll = await _context.Polls.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);
                if (Poll == null)
                    return false;
                _context.Polls.Update(PollConverter.Convert(item));
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
