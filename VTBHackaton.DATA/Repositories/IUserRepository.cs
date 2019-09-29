using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VTBHackaton.DATA.Dto;

namespace VTBHackaton.DATA.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllAsync();

        Task<UserDto> GetByIdAsync(Guid id);

        Task<UserDto> GetByEmailAsync(string email);

        Task<UserDto> CreateAsync(UserDto item);

        Task<bool> UpdateAsync(UserDto iten);

        Task<bool> DeleteByIdAsync(Guid id);

        Task<bool> DeleteByEmailAsync(string email);
    }
}
