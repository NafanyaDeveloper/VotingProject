using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VTBHackaton.DATA.Enteties
{
    public class Poll
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public Room Room { get; set; }

        public Guid RoomId { get; set; }

        public List<Variant> Variants { get; set; } = new List<Variant>();
    }
}
