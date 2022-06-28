namespace TruckRoadProject.Models.TrucksModels
{
    public class BlueTruck : ITruck
    {
        public int Id { get; set; }
        public string Type { get; set; } = "Blue";
        public int Capacity { get; set; } = 1500;
        public double Speed { get; set; } = 1.5;
        public int LoadTime { get; set; } = 3;
        public int UnloadTime { get; set; } = 24;
        public Warehouse StartingPosition { get; set; }
    }
}
