using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Hotels.Hotels
{
    public class HotelTranslationDto
    {
        public string Name { get; set; } = null!;
        public string? About { get; set; }
        public string? Address { get; set; }
        public int HotelId { get; set; }
        public int LanguageId { get; set; }
    }
}
