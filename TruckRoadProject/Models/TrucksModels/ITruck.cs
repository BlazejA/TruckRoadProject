namespace TruckRoadProject.Models.TrucksModels
{
    public interface ITruck
    {
        int Id { get; set; }
        string Type { get; set; }
        int Capacity { get; set; }
        double Speed { get; set; }
        int LoadTime { get; set; }
        int UnloadTime { get; set; }
        Warehouse StartingPosition { get; set; }
        
    }
}
