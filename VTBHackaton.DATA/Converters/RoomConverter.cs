using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTBHackaton.DATA.Dto;
using VTBHackaton.DATA.Enteties;

namespace VTBHackaton.DATA.Converters
{
    public static class RoomConverter
    {
        public static Room Convert(RoomDto item) =>
            new Room
            {
                Id = item.Id,
                CloseTime = item.CloseTime,
                CreationTime = item.CreationTime,
                Description = item.Description,
                Title = item.Title,
                AdminId = item.AdminId
            };

        public static RoomDto Convert(Room item) =>
            new RoomDto
            {
                Id = item.Id,
                CloseTime = item.CloseTime,
                CreationTime = item.CreationTime,
                Description = item.Description,
                Title = item.Title,
                AdminId = item.AdminId
            };

        public static List<Room> Convert(List<RoomDto> items) =>
            items.Select(x => Convert(x)).ToList();

        public static List<RoomDto> Convert(List<Room> items) =>
            items.Select(x => Convert(x)).ToList();
    }
}
