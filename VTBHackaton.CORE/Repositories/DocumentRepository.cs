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
    public class DocumentRepository : IDocumentRepository
    {
        private readonly VTBHackatonContext _context;

        public DocumentRepository(VTBHackatonContext context) => _context = context;

        public async Task<DocumentDto> CreateAsync(DocumentDto item)
        {
            try
            {
                if (item == null)
                    return null;
                var res = await _context.Documents.AddAsync(DocumentConverter.Convert(item));
                await _context.SaveChangesAsync();
                return DocumentConverter.Convert(res.Entity);
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
                var Document = await _context.Documents.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (Document == null)
                    return false;
                _context.Documents.Remove(Document);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<DocumentDto>> GetAllAsync()
        {
            try
            {
                return await _context.Documents.AsNoTracking().Select(z => new DocumentDto
                {
                    Id = z.Id,
                    Link = z.Link,
                    RoomId = z.RoomId,
                    Title = z.Title,
                    Room = RoomConverter.Convert(z.Room)
                }).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DocumentDto> GetById(Guid id)
        {
            try
            {
                return await _context.Documents.AsNoTracking().Select(z => new DocumentDto
                {
                    Id = z.Id,
                    Link = z.Link,
                    RoomId = z.RoomId,
                    Title = z.Title,
                    Room = RoomConverter.Convert(z.Room)
                }).FirstOrDefaultAsync(h => h.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(DocumentDto item)
        {
            try
            {
                if (item == null || item.Id == null)
                    return false;
                var Document = await _context.Documents.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);
                if (Document == null)
                    return false;
                _context.Documents.Update(DocumentConverter.Convert(item));
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
