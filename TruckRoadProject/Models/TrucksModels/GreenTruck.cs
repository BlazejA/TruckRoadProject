namespace TruckRoadProject.Models.TrucksModels
{
    public class GreenTruck : ITruck
    {
        public int Id { get; set; }
        public string Type { get; set; } = "Green";
        public int Capacity { get; set; } = 1000;
        public double Speed { get; set; } = 1.5;
        public int LoadTime { get; set; } = 1;
        public int UnloadTime { get; set; } = 24;
        public Warehouse StartingPosition { get; set; }
    }
}
