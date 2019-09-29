using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTBHackaton.CORE.EF;
using VTBHackaton.DATA.Enteties;
using VTBHackaton.DATA.Repositories;
using VTBHackaton.DATA.ViewModels;

namespace VTBHackaton.CORE.Repositories
{
    public class UserVariantRepository: IUserVariantRepository
    {
        private readonly VTBHackatonContext _context;

        public UserVariantRepository(VTBHackatonContext context) => _context = context;

        public async Task<bool> AddVariantsToUserAsync(UserVariantsViewModel item)
        {
            try
            {
                List<UserVariant> uv = new List<UserVariant>();
                var vrnts = await _context.Variants.AsNoTracking().Where(x => item.Variants.Contains(x.Id)).ToListAsync();
                if (vrnts.Count != item.Variants.Count)
                    return false;
                foreach (var i in vrnts)
                    if (vrnts.Where(x => x.PollId == i.PollId).Count() > 1)
                        return false;
                foreach(var i in vrnts)
                {
                    var poll = await _context.Polls.AsNoTracking().Select(x => new
                    {
                        RoomId = x.Room.Id,
                        Variants = x.Variants.Select(a => a.UserVariant).ToList(),
                        Id = x.Id
                    }).FirstAsync(y => y.Id == i.PollId);
                    var roomUser = await _context.UserRoom.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.RoomId == poll.RoomId && x.UserId == item.UserId);
                    if (roomUser == null)
                        return false;
                    foreach(var y in poll.Variants)
                    {
                        var vu = y.FirstOrDefault(x => x.UserId == item.UserId);
                        if (vu != null)
                            return false;
                    }
                    uv.Add(new UserVariant { UserId = item.UserId, VariantId = i.Id });
                }
               /* var ur = item.Variants.Select(x => new UserVariant
                {
                    VariantId = x,
                    UserId = item.UserId
                }).ToList();*/
                await _context.UserVariant.AddRangeAsync(uv);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddUsersToVariantAsync(VariantUsersViewModel item)
        {
            try
            {
                var res = new List<UserVariant>();
                var variant = await _context.Variants.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.VariantId);
                if (variant == null)
                    return false;
                var poll = await _context.Polls.AsNoTracking().Select(x => new
                {
                    RoomId = x.Room.Id,
                    Variants = x.Variants.Select(a => a.UserVariant).ToList(),
                    Id = x.Id
                }).FirstAsync(y => y.Id == variant.PollId);
                var users = await _context.Users.AsNoTracking().Where(x => item.Users.Contains(x.Id)).ToListAsync();
                foreach(var u in users)
                {
                    var usrRoom = await _context.UserRoom.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.UserId == u.Id && x.RoomId == poll.RoomId);
                    if (usrRoom == null)
                        return false;
                    foreach(var v in poll.Variants)
                    {
                        var vu = v.FirstOrDefault(x => x.UserId == u.Id);
                        if (vu != null)
                            return false;
                    }
                    res.Add(new UserVariant { VariantId = item.VariantId, UserId = u.Id });
                }
                await _context.UserVariant.AddRangeAsync(res);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteVariantsFromUserAsync(UserVariantsViewModel item)
        {
            try
            {
                var ur = item.Variants.Select(x => new UserVariant
                {
                    VariantId = x,
                    UserId = item.UserId
                }).ToList();
                _context.UserVariant.RemoveRange(ur);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUsersFromVariantAsync(VariantUsersViewModel item)
        {
            try
            {
                var ur = item.Users.Select(x => new UserVariant
                {
                    VariantId = item.VariantId,
                    UserId = x
                }).ToList();
                _context.UserVariant.RemoveRange(ur);
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
