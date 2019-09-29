using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VTBHackaton.DATA.ViewModels;

namespace VTBHackaton.DATA.Repositories
{
    public interface IUserRoomRepository
    {
        Task<bool> AddRoomsToUserAsync(UserRoomsViewModel item);

        Task<bool> AddUsersToRoomAsync(RoomUsersViewModel item);

        Task<bool> DeleteRoomsFromUserAsync(UserRoomsViewModel item);

        Task<bool> DeleteUsersFromRoomAsync(RoomUsersViewModel item);
    }
}
