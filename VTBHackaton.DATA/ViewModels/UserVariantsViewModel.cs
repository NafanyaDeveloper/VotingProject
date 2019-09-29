using System;
using System.Collections.Generic;
using System.Text;

namespace VTBHackaton.DATA.ViewModels
{
    public class UserVariantsViewModel
    {
        public Guid UserId { get; set; }

        public List<Guid> Variants { get; set; } = new List<Guid>();
    }
}
