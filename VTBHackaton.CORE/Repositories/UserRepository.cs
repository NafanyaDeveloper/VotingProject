using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTBHackaton.CORE.EF;
using VTBHackaton.DATA.Converters;
using VTBHackaton.DATA.Dto;
using VTBHackaton.DATA.Enteties;
using VTBHackaton.DATA.Repositories;

namespace VTBHackaton.CORE.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly VTBHackatonContext _context;

        public UserRepository(VTBHackatonContext context) => _context = context;

        public async Task<UserDto> CreateAsync(UserDto item)
        {
            try
            {
                if (item == null || item.Email == null || item.Password == null)
                    return null;
                var res = await _context.Users.AddAsync(UserConverter.Convert(item));
                await _context.SaveChangesAsync();
                return UserConverter.Convert(res.Entity);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteByEmailAsync(string email)
        {
            try
            {
                if (email == null)
                    return false;
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email);
                if (user == null)
                    return false;
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            try
            {
                if (id == null)
                    return false;
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                    return false;
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            try
            {
                List<RoomDto> rooms = RoomConverter.Convert(await _context.Rooms.AsNoTracking().ToListAsync());
                var users = UserConverter.Convert(await _context.Users.AsNoTracking().ToListAsync());
                var userroom = await _context.UserRoom.AsNoTracking().ToListAsync();
                var variants = VariantConverter.Convert(await _context.Variants.AsNoTracking().ToListAsync());
                var variantuser = await _context.UserVariant.AsNoTracking().ToListAsync();
                foreach (var user in users)
                {
                    var uv = variantuser.Where(x => x.UserId == user.Id);
                    foreach (var i in uv)
                        user.Variants.Add(variants.First(x => x.Id == i.VariantId));

                    var ur = userroom.Where(x => x.UserId == user.Id);
                    foreach (var i in ur)
                        user.Rooms.Add(rooms.First(x => x.Id == i.RoomId));
                }
                return users;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            try
            {
                var user = UserConverter.Convert(await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email == email));
                var userroom = await _context.UserRoom.AsNoTracking().Where(x => x.UserId == user.Id).ToListAsync();
                var uservariant = await _context.UserVariant.AsNoTracking().Where(x => x.UserId == user.Id).ToListAsync();
                foreach (var i in userroom)
                    user.Rooms.Add(RoomConverter.Convert(await _context.Rooms.AsNoTracking().FirstAsync(x => x.Id == i.RoomId)));

                foreach (var i in uservariant)
                    user.Variants.Add(VariantConverter.Convert(await _context.Variants.AsNoTracking().FirstAsync(x => x.Id == i.VariantId)));

                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = UserConverter.Convert(await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id));
            var userroom = await _context.UserRoom.AsNoTracking().Where(x => x.UserId == user.Id).ToListAsync();
            var uservariant = await _context.UserVariant.AsNoTracking().Where(x => x.UserId == user.Id).ToListAsync();
            foreach (var i in userroom)
                user.Rooms.Add(RoomConverter.Convert(await _context.Rooms.AsNoTracking().FirstAsync(x => x.Id == i.RoomId)));

            foreach (var i in uservariant)
                user.Variants.Add(VariantConverter.Convert(await _context.Variants.AsNoTracking().FirstAsync(x => x.Id == i.VariantId)));
            return user;
        }

        public async Task<bool> UpdateAsync(UserDto item)
        {
            try
            {
                if (item == null || item.Id == null)
                    return false;
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);
                if (user == null)
                    return false;
                user.Email = item.Email;
                user.Name = item.Name;
                user.UserName = item.Email;
                user.PhoneNumber = item.PhoneNumber;
                user.Surname = item.Surname;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
