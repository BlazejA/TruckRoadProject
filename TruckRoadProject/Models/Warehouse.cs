namespace TruckRoadProject.Models
{
    public class Warehouse
    {
        public string Id { get; set; }
        /// <summary>
        /// Random 100-200 kg
        /// </summary>
        public int LoadAmount { get; set; }
        /// <summary>
        /// True if deliver enough load amount
        /// </summary>
        public bool IsFull { get; set; }
    }
}
