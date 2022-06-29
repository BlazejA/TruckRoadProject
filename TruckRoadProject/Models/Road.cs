using TruckRoadProject.Models.TrucksModels;

namespace TruckRoadProject.Models
{
    public class Road
    {
        public Map SingleRoad { get; set; } //TODO: zmienić na mappoint i lodować ładunek
        public string Load { get; set; }
        public IEnumerable<ITruck> Trucks { get; set; }
        public int Time { get; set; }

    }
}
