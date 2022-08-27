using Domain.Entities.Localization;

namespace Domain.Entities.TourServices.Translation
{
    public  class TourServiceTranslation
    {
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
