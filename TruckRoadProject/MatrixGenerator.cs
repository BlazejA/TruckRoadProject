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

                for (var j = 0; j <= points.Points.Count - 1; j++)
                {
                    if (i < j)
                    {
                        continue;
                    }
                    if( DistancesMatrix[i,j] == " ") continue;
                    
                    DistancesMatrix[i, j] = MapGenerator.CountDistanceOfTwoPoints(points.Points[i], points.Points[j]).ToString();

                }
            }

            return DistancesMatrix;
        }

        public static void GenerateFile()
        {
            File.WriteAllLines(Environment.CurrentDirectory + @"\trasa.txt", MakeOneDimension(DistancesMatrix));
        }

        private static List<string> MakeOneDimension(string[,] twoDim)
        {
            var list = new List<string>();
            list.Add(twoDim.GetLength(0).ToString());
            for (var i = 0; i < twoDim.GetLength(0); i++)
            {
                string line = null;
                for (var j = 0; j < twoDim.GetLength(1); j++)
                {
                    if (twoDim[i,j] != null)
                    {
                        line += twoDim[i, j] + " "; 
                    }
                    
                }
                list.Add(line);
            }

            return list;
        }
    }
}
