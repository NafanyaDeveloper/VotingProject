using System;
using System.Collections.Generic;
using System.Text;

namespace VTBHackaton.DATA.Dto
{
    public class DocumentDto
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public RoomDto Room { get; set; }
    }
}
