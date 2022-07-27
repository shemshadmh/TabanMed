
namespace Domain.Entities.Destination
{
    public class Country
    {
        public int Id { get; set; }
        public string FaName { get; set; } = null!;
        public string EnName { get; set; } = null!;

        #region Relations

        // Cities
        public ICollection<City>? Cities { get; set; }

        #endregion
    }
}
