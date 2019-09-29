using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VTBHackaton.DATA.Dto;

namespace VTBHackaton.DATA.Repositories
{
    public interface IRoomRepository
    {
        Task<List<RoomDto>> GetAllAsync();

        Task<RoomDto> GetById(Guid id);

        Task<RoomDto> CreateAsync(RoomDto item);

        Task<bool> UpdateAsync(RoomDto item);

        Task<bool> DeleteAsync(Guid id);
    }
}
