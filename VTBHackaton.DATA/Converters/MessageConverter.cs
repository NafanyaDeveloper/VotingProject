using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTBHackaton.DATA.Dto;
using VTBHackaton.DATA.Enteties;

namespace VTBHackaton.DATA.Converters
{
    public static class MessageConverter
    {
        public static Message Convert(MessageDto item) =>
            new Message
            {
                Id = item.Id,
                Date = item.Date,
                UserId = item.UserId,
                RoomId = item.RoomId,
                Text = item.Text
            };

        public static MessageDto Convert(Message item) =>
            new MessageDto
            {
                Id = item.Id,
                Date = item.Date,
                UserId = item.UserId,
                RoomId = item.RoomId,
                Text = item.Text
            };

        public static List<Message> Convert(List<MessageDto> items) =>
            items.Select(x => Convert(x)).ToList();

        public static List<MessageDto> Convert(List<Message> items) =>
            items.Select(x => Convert(x)).ToList();
    }
}

