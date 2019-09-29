using System;
using System.Collections.Generic;
using System.Text;

namespace VTBHackaton.DATA.Dto
{
    public class VariantDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public PollDto Poll { get; set; }

        public Guid PollId { get; set; }

        public List<UserDto> Users { get; set; } = new List<UserDto>();
    }
}
