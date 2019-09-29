using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VTBHackaton.DATA.Dto;

namespace VTBHackaton.DATA.Repositories
{
    public interface IDocumentRepository
    {
        Task<List<DocumentDto>> GetAllAsync();

        Task<DocumentDto> GetById(Guid id);

        Task<DocumentDto> CreateAsync(DocumentDto item);

        Task<bool> UpdateAsync(DocumentDto item);

        Task<bool> DeleteAsync(Guid id);
    }
}
