namespace HotelHub.Domain.Rooms;

public sealed record RoomType(string Value)
{
    private static readonly RoomType Single = new RoomType("Single");
    private static readonly RoomType Double = new RoomType("Double");
    private static readonly RoomType Triple = new RoomType("Triple");
    private static readonly RoomType King = new RoomType("King");
    private static readonly RoomType Studio = new RoomType("Studio");
    private static readonly RoomType Suite = new RoomType("Suite");
    private static readonly RoomType MasterSuite = new RoomType("MasterSuite");
    private static readonly RoomType PresidentialSuite = new RoomType("PresidentialSuite");
    
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
}