using TruckRoadProject.Models;

namespace TruckRoadProject
{
    public class MapGenerator
    {
        Random random = new Random();

        public List<MapPoint> ShortestRoads { get; set; } = new List<MapPoint>();

        public List<(double result, MapPoint x1, MapPoint x2)> PointsWithDistance { get; set; } = new List<(double result, MapPoint x1, MapPoint x2)>();

        public Map MapPointsGenerator()
        {
            var map = new Map();
            var end = random.Next(400, 601);
            for (var i = 0; i < 5; i++)
            {
                var point = new MapPoint
                {
                    X = random.Next(101),
                    Y = random.Next(101)
                };
                map.Points.Add(point);
            }
            return map;
        }

        public void CountShortestWays(List<MapPoint> map, MapPoint current)
        {
            if (ShortestRoads.Count != 0)
            {
                var resultList = new List<KeyValuePair<double, MapPoint>>();
                foreach (var item in map)
                {
                    if (current == item) continue;
                    var countX = Math.Pow(item.X - current.X, 2);
                    var countY = Math.Pow(item.Y - current.Y, 2);
                    var result = Math.Sqrt(countX + countY);
                    resultList.Add(new KeyValuePair<double, MapPoint>(result, item));
                }
                ShortestRoads.Add(FoundShortestWay(resultList));
            }
            else
            {
                ShortestRoads.Add(current);
            }
        }

        public List<(double result, MapPoint x1, MapPoint x2)> CountDistance(List<MapPoint> map)
        {
            var resultList = new List<(double result, MapPoint, MapPoint)>();
            for (var i = 1; i < map.Count; i++)
            {
                var countX = Math.Pow(map[i].X - map[i - 1].X, 2);
                var countY = Math.Pow(map[i].Y - map[i - 1].Y, 2);
                var result = Math.Sqrt(countX + countY);
                resultList.Add((result, map[i - 1], map[i]));
            }

            return resultList;
        }

        public static double CountDistanceOfTwoPoints(MapPoint first, MapPoint second)
        {
            var countX = Math.Pow(second.X - first.X, 2);
            var countY = Math.Pow(second.Y - first.Y, 2);
            var result = Math.Sqrt(countX + countY);

            return result;
        }

        public MapPoint FoundShortestWay(List<KeyValuePair<double, MapPoint>> ways)
        {
            var shortestObj = new MapPoint();
            var shortest = ways[0].Key;
            foreach (var item in ways.Where(item => item.Key < shortest))
            {
                shortest = item.Key;
                shortestObj = item.Value;
            }

            return shortestObj;
        }

    }
}
