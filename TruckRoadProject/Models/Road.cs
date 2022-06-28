using TruckRoadProject.Models.TrucksModels;

namespace TruckRoadProject.Models
{
    public class Road
    {
        IEnumerable<SingleRoad> SingleRoads { get; set; }
        string Load { get; set; }
        ITruck Truck { get; set; }

    }
}
