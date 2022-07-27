
namespace Domain.Entities.Destination
{
    public class City 
    {
        public int Id { get; set; }
        public string FaName { get; set; } = null!;
        public string EnName { get; set; } = null!;

        #region Relations

        // Country
        public int CountryId { get; set; }
        public Country Country { get; set; } = null!;


        #endregion
    }
}
