using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VTBHackaton.DATA.Dto;

namespace VTBHackaton.DATA.Repositories
{
    public interface IPollRepository
    {
        Task<List<PollDto>> GetAllAsync();

        Task<PollDto> GetById(Guid id);

        Task<PollDto> CreateAsync(PollDto item);

        Task<bool> UpdateAsync(PollDto item);

        Task<bool> DeleteAsync(Guid id);
    }
}
