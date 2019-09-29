using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VTBHackaton.DATA.ViewModels;

namespace VTBHackaton.DATA.Repositories
{
    public interface IUserVariantRepository
    {
        Task<bool> AddVariantsToUserAsync(UserVariantsViewModel item);

        Task<bool> AddUsersToVariantAsync(VariantUsersViewModel item);

        Task<bool> DeleteVariantsFromUserAsync(UserVariantsViewModel item);

        Task<bool> DeleteUsersFromVariantAsync(VariantUsersViewModel item);
    }
}
