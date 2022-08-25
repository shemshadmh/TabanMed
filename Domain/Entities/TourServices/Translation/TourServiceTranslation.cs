using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.Localization;

namespace Domain.Entities.TourServices.Translation
{
    public  class TourServiceTranslation
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string?  Description { get; set; } 


        // Tour Service
        public int TourServiceId { get; set; }
        public TourService TourService { get; set; } = null!;

        // Language
        public int LanguageId { get; set; }
        public Language Language { get; set; } = null!;
    }
}
