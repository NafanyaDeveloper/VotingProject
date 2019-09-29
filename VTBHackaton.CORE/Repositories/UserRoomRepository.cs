using Microsoft.EntityFrameworkCore;
using NETCore.MailKit.Core;
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
    public class UserRoomRepository : IUserRoomRepository
    {
        private readonly VTBHackatonContext _context;

        private readonly IEmailService _EmailService;
        public UserRoomRepository(VTBHackatonContext context, IEmailService sender)
        {
            _context = context;
            _EmailService = sender;
        }

        public async Task<bool> AddRoomsToUserAsync(UserRoomsViewModel item)
        {
            try
            {
                var ur = item.Rooms.Select(x => new UserRoom
                {
                    RoomId = x,
                    UserId = item.UserId
                }).ToList();
                User user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.UserId);
                await _context.UserRoom.AddRangeAsync(ur);
                await _context.SaveChangesAsync();
                string text = String.Format(" Здравствуйте, {0} {1}!!!\n Вы были добавлены в комнату для обсуждения" +
                    " новых документов.\n \n С уважением, ваш сервис Femida :)", user.Name, user.Surname);
                _EmailService.Send(user.Email, "Добавление в комнату!", text);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddUsersToRoomAsync(RoomUsersViewModel item)
        {
            try
            {
                var ur = item.Users.Select(x => new UserRoom
                {
                    RoomId = item.RoomId,
                    UserId = x
                }).ToList();
                await _context.UserRoom.AddRangeAsync(ur);
                await _context.SaveChangesAsync();
                foreach(var u in item.Users)
                {
                    User user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == u);
                    string text = String.Format(" Здравствуйте, {0} {1}!!!\n Вы были добавлены в комнату для обсуждения" +
                    " новых документов.\n \n С уважением, ваш сервис Femida :)", user.Name, user.Surname);
                    _EmailService.Send(user.Email, "Добавление в комнату!", text);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteRoomsFromUserAsync(UserRoomsViewModel item)
        {
            try
            {
                var ur = item.Rooms.Select(x => new UserRoom
                {
                    RoomId = x,
                    UserId = item.UserId
                }).ToList();
                _context.UserRoom.RemoveRange(ur);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUsersFromRoomAsync(RoomUsersViewModel item)
        {
            try
            {
                var ur = item.Users.Select(x => new UserRoom
                {
                    RoomId = item.RoomId,
                    UserId = x
                }).ToList();
                _context.UserRoom.RemoveRange(ur);
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
