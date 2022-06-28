using TruckRoadProject.Models;

namespace TruckRoadProject
{
    public class MatrixGenerator
    {
        private static string[,] DistancesMatrix { get; set; }
        public static string[,] CreateMatrix(Map points)
        {
            DistancesMatrix = new string[points.Points.Count, points.Points.Count];

            for (var i = 0; i <= points.Points.Count - 1; i++)
            {
                if (i != 0) DistancesMatrix[i, 0] = Helpers.PointToString(points.Points[i]);
                else DistancesMatrix[0, 0] = "-\t";

                for (var j = 1; j <= points.Points.Count - 1; j++)
                {
                    if (i > 0) DistancesMatrix[i, j] = MapGenerator
                            .CountDistanceOfTwoPoints(points.Points[i], points.Points[j])
                            .ToString("N2");
                    else DistancesMatrix[0, j] = Helpers.PointToString(points.Points[j]);
                }
            }

            return DistancesMatrix;
        }
    }
}
