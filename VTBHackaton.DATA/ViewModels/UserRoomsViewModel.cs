using System;
using System.Collections.Generic;
using System.Text;

namespace VTBHackaton.DATA.ViewModels
{
    public class UserRoomsViewModel
    {
        public Guid UserId { get; set; }

        public List<Guid> Rooms { get; set; } = new List<Guid>();
    }
}
