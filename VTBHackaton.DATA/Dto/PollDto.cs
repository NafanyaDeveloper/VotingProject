using System;
using System.Collections.Generic;
using System.Text;

namespace VTBHackaton.DATA.Dto
{
    public class PollDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public RoomDto Room { get; set; }

        public Guid RoomId { get; set; }

        public List<VariantDto> Variants { get; set; } = new List<VariantDto>();
    }
}
