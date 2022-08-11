namespace Application.Dtos.Hotels.Hotels;
public class HotelDetailsDto
{
    public int Id { get; set; }
    public string FaName { get; set; } = null!;
    public string EnName { get; set; } = null!;
    public int Rate { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string? About { get; set; }
    public int Stars { get; set; }
    public string? OtherNames { get; set; }
    public int? RoomCount { get; set; }
    public string? Address { get; set; }
    public string? CallInformation { get; set; }
    public string? WebsiteAddress { get; set; }

    #region Location

    public double Lat { get; set; }
    public double Lng { get; set; }
    public byte Zoom { get; set; }

    #endregion

    public string CityFaName { get; set; } = null!;

    public string CityEnName { get; set; } = null!;
    public int CountPendingComments { get; set; }
}