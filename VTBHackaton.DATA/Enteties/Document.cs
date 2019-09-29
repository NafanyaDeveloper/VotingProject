using System;
using System.Collections.Generic;
using System.Text;

namespace VTBHackaton.DATA.Enteties
{
    public class Document
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public Room Room { get; set; }
    }
}
