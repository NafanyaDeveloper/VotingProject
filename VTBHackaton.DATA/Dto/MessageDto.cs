using System;
using System.Collections.Generic;
using System.Text;

namespace VTBHackaton.DATA.Dto
{
    public class MessageDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public UserDto User { get; set; }

        public Guid RoomId { get; set; }

        public RoomDto Room { get; set; }
    }
}
