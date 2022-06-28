namespace TruckRoadProject.Models.TrucksModels
{
    public class RedTruck : ITruck
    {
        public int Id { get; set; }
        public string Type { get; set; } = "Red";
        public int Capacity { get; set; } = 2000;
        public double Speed { get; set; } = 0.75;
        public int LoadTime { get; set; } = 3;
        public int UnloadTime { get; set; } = 24;
        public Warehouse StartingPosition { get; set; }
    }
}
