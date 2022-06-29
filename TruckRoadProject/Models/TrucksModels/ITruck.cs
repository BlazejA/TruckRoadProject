namespace TruckRoadProject.Models.TrucksModels
{
    public interface ITruck
    {
        int Id { get; set; }
        /// <summary>
        /// Truck type
        /// </summary>
        string Type { get; set; }
        /// <summary>
        /// Amount of products
        /// </summary>
        int Capacity { get; set; }
        /// <summary>
        /// Time in seconds
        /// </summary>
        int LoadTime { get; set; }
        /// <summary>
        /// Time in seconds
        /// </summary>
        int UnloadTime { get; set; }
        
    }
}
