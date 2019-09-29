using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VTBHackaton.DATA.Enteties
{
    public class Room
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime CloseTime { get; set; }

        public List<UserRoom> UserRoom { get; set; } = new List<UserRoom>();

        public Guid AdminId { get; set; }

        public List<Poll> Polls { get; set; } = new List<Poll>();

        public List<Document> Documents { get; set; } = new List<Document>();

        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
