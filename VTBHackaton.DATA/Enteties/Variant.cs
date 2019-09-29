using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VTBHackaton.DATA.Enteties
{
    public class Variant
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public Guid PollId { get; set; }

        public Poll Poll { get; set; }

        public List<UserVariant> UserVariant { get; set; } = new List<UserVariant>();
    }
}
