
using Domain.Entities.TourServices.Translation;

namespace Domain.Entities.TourServices
{
    public class TourService
    {
        public int Id { get; set; }


        #region Relations
        //Translation
        public ICollection<TourServiceTranslation>? TourServiceTranslation { get; set; }

        #endregion
    }
}
