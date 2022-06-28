using TruckRoadProject.Models;

namespace TruckRoadProject
{
    public class Helpers
    {
        public static string PointToString(MapPoint element) => $"X:{element.X} Y:{element.Y}";
    }
}
