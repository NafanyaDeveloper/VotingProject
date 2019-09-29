using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTBHackaton.DATA.Dto;
using VTBHackaton.DATA.Enteties;

namespace VTBHackaton.DATA.Converters
{
    public static class PollConverter
    {
        public static Poll Convert(PollDto item) =>
            new Poll
            {
                Id = item.Id,
                RoomId = item.RoomId,
                Title = item.Title
            };

        public static PollDto Convert(Poll item) =>
            new PollDto
            {
                Id = item.Id,
                RoomId = item.RoomId,
                Title = item.Title
            };

        public static List<Poll> Convert(List<PollDto> items) =>
            items.Select(x => Convert(x)).ToList();

        public static List<PollDto> Convert(List<Poll> items) =>
            items.Select(x => Convert(x)).ToList();
    }
}
