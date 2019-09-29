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
    public class VariantRepository: IVariantRepository
    {
        private readonly VTBHackatonContext _context;

        public VariantRepository(VTBHackatonContext context) => _context = context;

        public async Task<VariantDto> CreateAsync(VariantDto item)
        {
            try
            {
                if (item == null)
                    return null;
                var res = await _context.Variants.AddAsync(VariantConverter.Convert(item));
                await _context.SaveChangesAsync();
                return VariantConverter.Convert(res.Entity);
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
                var Variant = await _context.Variants.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (Variant == null)
                    return false;
                _context.Variants.Remove(Variant);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<VariantDto>> GetAllAsync()
        {
            try
            {
                return await _context.Variants.AsNoTracking().Select(a => new VariantDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Users = UserConverter.Convert(a.UserVariant.Select(b => b.User).ToList()),
                    PollId = a.PollId,
                    Poll = PollConverter.Convert(a.Poll)
                }).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<VariantDto> GetById(Guid id)
        {
            try
            {
                return await _context.Variants.AsNoTracking().Select(a => new VariantDto
                {
                    Id = a.Id,
                    Title = a.Title,
                    Users = UserConverter.Convert(a.UserVariant.Select(b => b.User).ToList()),
                    PollId = a.PollId,
                    Poll = PollConverter.Convert(a.Poll)
                }).FirstOrDefaultAsync(h => h.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(VariantDto item)
        {
            try
            {
                if (item == null || item.Id == null)
                    return false;
                var Variant = await _context.Variants.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);
                if (Variant == null)
                    return false;
                _context.Variants.Update(VariantConverter.Convert(item));
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
