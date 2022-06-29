using TruckRoadProject.Models;

namespace TruckRoadProject
{
    public class TruckRide
    {
        private void Ride()
        {
            var road = new Road();

            foreach (var truck in road.Trucks)
            {
                foreach (var item in road.SingleRoad.Points.Where(item => !item.Warehouse.IsFull))
                {
                    while (truck.Capacity != 0)
                    {
                        if(truck.Capacity>=item.Warehouse.LoadAmount)
                        {
                            truck.Capacity -= item.Warehouse.LoadAmount;
                            item.Warehouse.LoadAmount = 0;
                            item.Warehouse.IsFull = true;
                        }
                        else
                        {
                            item.Warehouse.LoadAmount -= truck.Capacity;
                            truck.Capacity = 0;
                        }
                    }
                }
            }
        }

    }
}
