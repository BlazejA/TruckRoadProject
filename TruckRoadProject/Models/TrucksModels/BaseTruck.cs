namespace TruckRoadProject.Models.TrucksModels
{
    public class BaseTruck : ITruck
    {
        public int Id { get; set; }
        public string Type { get; set; } = "Base";
        public int Capacity { get; set; } = 1000;
        public int LoadTime { get; set; } = 2;
        public int UnloadTime { get; set; } = 1;

        public BaseTruck(){}

        public BaseTruck(int capacity)
        {
            Capacity = capacity;
        }
    }
}
