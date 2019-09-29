using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTBHackaton.DATA.Dto;
using VTBHackaton.DATA.Enteties;

namespace VTBHackaton.DATA.Converters
{
    public static class DocumentConverter
    {
        public static Document Convert(DocumentDto item) =>
            new Document
            {
                Id = item.Id,
                Link = item.Link,
                RoomId = item.RoomId,
                Title = item.Title
            };

        public static DocumentDto Convert(Document item) =>
            new DocumentDto
            {
                Id = item.Id,
                Link = item.Link,
                RoomId = item.RoomId,
                Title = item.Title
            };

        public static List<Document> Convert(List<DocumentDto> items) =>
            items.Select(a => Convert(a)).ToList();

        public static List<DocumentDto> Convert(List<Document> items) =>
            items.Select(a => Convert(a)).ToList();
    }
}
