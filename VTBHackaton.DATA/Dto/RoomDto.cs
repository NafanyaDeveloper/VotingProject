using System;
using System.Collections.Generic;
using System.Text;
using VTBHackaton.DATA.Enteties;

namespace VTBHackaton.DATA.Dto
{
    public class RoomDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime CloseTime { get; set; }

        public List<UserDto> Users { get; set; } = new List<UserDto>();

        public List<PollDto> Polls { get; set; } = new List<PollDto>();

        public UserDto Admin { get; set; }

        public Guid AdminId { get; set; }

        public List<DocumentDto> Documents { get; set; } = new List<DocumentDto>();

        public List<MessageDto> Messages { get; set; } = new List<MessageDto>();
    }
}
