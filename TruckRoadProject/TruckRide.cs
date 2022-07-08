using System.Diagnostics;
using TruckRoadProject.Models;
using TruckRoadProject.Models.TrucksModels;

namespace TruckRoadProject
{
    public class TruckRide
    {
        private static Map RoadMap { get; set; } = new Map();
        public static void Ride(Road road)
        {
            MakeMapFromRoad(road);
            Debug.WriteLine("Trucks:");
            foreach (var item in road.Trucks)
            {
                Debug.WriteLine(item.Capacity);
            }
            var truck = road.Trucks.Peek();
            foreach (var item in RoadMap.Points.Where(item => !item.Warehouse.IsFull))
            {
                while (truck.Capacity != 0)
                {
                    if (truck.Capacity >= item.Warehouse.LoadAmount && item.Warehouse.LoadAmount!=0)
                    {
                        road.Time += item.Warehouse.LoadAmount;
                        truck.Capacity -= item.Warehouse.LoadAmount;
                        item.Warehouse.LoadAmount = 0;
                        item.Warehouse.IsFull = true;
                    }
                    else if (item.Warehouse.LoadAmount == 0)
                    {
                        break;
                    }
                    else
                    {
                        road.Time += truck.Capacity;
                        item.Warehouse.LoadAmount -= truck.Capacity;
                        truck.Capacity = 0;
                        road.Trucks.Dequeue();
                        var total = RoadMap.Points.Where(x => !x.Warehouse.IsFull).Sum(x => x.Warehouse.LoadAmount);
                        if (total>100)
                        {
                            road.Trucks.Enqueue(new BaseTruck());
                            road.Time += 2 * 1000;
                        }
                        else
                        {
                            road.Trucks.Enqueue(new BaseTruck(total));
                            road.Time += 2 * total;
                        }
                        road.Time += 2 * 1000;
                        truck = road.Trucks.Peek();
                    }
                }
            }

        }

        private static void MakeMapFromRoad(Road road)
        {
            foreach (var point in road.RoadPoint)
            {
                RoadMap.Points.Add(road.Map.Points[point]);
            }
        }

    }
}
