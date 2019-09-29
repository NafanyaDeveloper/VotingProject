using System;
using System.Collections.Generic;
using System.Text;

namespace VTBHackaton.DATA.ViewModels
{
    public class VariantUsersViewModel
    {
        public Guid VariantId { get; set; }

        public List<Guid> Users { get; set; } = new List<Guid>();
    }
}
