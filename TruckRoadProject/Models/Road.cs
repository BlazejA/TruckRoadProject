using TruckRoadProject.Models.TrucksModels;

namespace TruckRoadProject.Models
{
    public class Road
    {
        public Map Map { get; set; }
        public List<int> RoadPoint { get; set; }
        public Queue<ITruck> Trucks { get; set; }
        public int Time { get; set; } = 0;

        public Road(Map map, List<int> point, Queue<ITruck> trucks)
        {
            Map = map;
            RoadPoint = point;
            Trucks = trucks;
        }

    }
}
