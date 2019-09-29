using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTBHackaton.DATA.Dto;
using VTBHackaton.DATA.Enteties;

namespace VTBHackaton.DATA.Converters
{
    public static class VariantConverter
    {
        public static List<Variant> Convert(List<VariantDto> items) =>
            items.Select(x => Convert(x)).ToList();

        public static List<VariantDto> Convert(List<Variant> items) =>
            items.Select(x => Convert(x)).ToList();

        public static VariantDto Convert(Variant item) =>
            new VariantDto
            {
                Id = item.Id,
                PollId = item.PollId,
                Title = item.Title
            };

        public static Variant Convert(VariantDto item) =>
            new Variant
            {
                Id = item.Id,
                PollId = item.PollId,
                Title = item.Title
            };
    }
}
