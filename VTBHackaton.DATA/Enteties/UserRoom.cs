using System;
using System.Collections.Generic;
using System.Text;

namespace VTBHackaton.DATA.Enteties
{
    public class UserRoom
    {
        public Guid UserId { get; set; }

        public User User { get; set; }

        public Guid RoomId { get; set; }

        public Room Room { get; set; }
    }
}
