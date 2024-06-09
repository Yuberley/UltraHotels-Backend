namespace HotelHub.Domain.Rooms;

public sealed record RoomType(string Value)
{
    public static readonly RoomType Single = new RoomType("Single");
    public static readonly RoomType Double = new RoomType("Double");
    public static readonly RoomType Triple = new RoomType("Triple");
    public static readonly RoomType King = new RoomType("King");
    public static readonly RoomType Studio = new RoomType("Studio");
    public static readonly RoomType Suite = new RoomType("Suite");
    public static readonly RoomType MasterSuite = new RoomType("MasterSuite");
    public static readonly RoomType PresidentialSuite = new RoomType("PresidentialSuite");
    
    public static RoomType FromValue(string value)
    {
        return All.FirstOrDefault(c => c.Value == value) ??
               throw new ApplicationException("The room type value is invalid");
    }
    
    public static readonly IReadOnlyCollection<RoomType> All = new[]
    {
        Single,
        Double,
        Triple,
        King,
        Studio,
        Suite,
        MasterSuite,
        PresidentialSuite
    };
};