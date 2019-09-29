using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VTBHackaton.DATA.Enteties
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public RoleType RoleType { get; set; }

        public List<UserRoom> UserRoom { get; set; } = new List<UserRoom>();

        public List<UserVariant> UserVariant { get; set; } = new List<UserVariant>();

        public List<RefreshToken> Tokens { get; set; } = new List<RefreshToken>();

        public List<Message> Messages { get; set; } = new List<Message>();
    }

}
