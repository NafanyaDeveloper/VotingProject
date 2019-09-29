using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VTBHackaton.DATA.Dto;

namespace VTBHackaton.DATA.Repositories
{
    public interface IVariantRepository
    {
        Task<List<VariantDto>> GetAllAsync();

        Task<VariantDto> GetById(Guid id);

        Task<VariantDto> CreateAsync(VariantDto item);

        Task<bool> UpdateAsync(VariantDto item);

        Task<bool> DeleteAsync(Guid id);
    }
}
