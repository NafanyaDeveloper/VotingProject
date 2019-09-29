using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTBHackaton.DATA.Dto;
using VTBHackaton.DATA.Enteties;

namespace VTBHackaton.DATA.Converters
{
    public static class UserConverter
    {
        public static User Convert(UserDto item) =>
            new User
            {
                Id = item.Id,
                Email = item.Email,
                Name = item.Name,
                Surname = item.Surname,
                UserName = item.Email,
                PhoneNumber = item.PhoneNumber,
                RoleType = item.RoleType
            };

        public static UserDto Convert(User item) =>
            new UserDto
            {
                Id = item.Id,
                Email = item.Email,
                Name = item.Name,
                Surname = item.Surname,
                PhoneNumber = item.PhoneNumber,
                RoleType = item.RoleType
            };

        public static List<User> Convert(List<UserDto> items) =>
            items.Select(u => Convert(u)).ToList();

        public static List<UserDto> Convert(List<User> items) =>
            items.Select(u => Convert(u)).ToList();
    }
}
