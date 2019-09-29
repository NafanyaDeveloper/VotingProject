using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using VTBHackaton.DATA.Enteties;

namespace VTBHackaton.DATA.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Password { get; set; }

        public RoleType RoleType { get; set; }

        public string PhoneNumber { get; set; }

        public List<RoomDto> Rooms { get; set; } = new List<RoomDto>();

        public List<VariantDto> Variants { get; set; } = new List<VariantDto>();
    }
}
