using TruckRoadProject;


var generator = new MapGenerator();
var list = generator.MapPointsGenerator();

foreach (var item in list.Points)
{
    Console.Write("X " + item.X);
    Console.WriteLine(" Y " + item.Y);
}

//var dist = generator.CountDistance(list.Points);

//foreach (var (result, x1, x2) in dist)
//{
//    Console.WriteLine("res: "+ result + " x1: " + x1.X +" "+ x1.Y + " x2: " + x2.X + " " + x2.Y);
//}

//Console.WriteLine("---------------------------------\n");
var matrix = MatrixGenerator.CreateMatrix(list);

for (var i = 0; i < matrix.GetLength(0); i++)
{
    for (var j = 0; j < matrix.GetLength(1); j++)
    {
        if (i == 0 || j == 0) Console.Write(matrix[i, j] + "\t");
        else Console.Write(matrix[i, j] + "\t\t");
    }
    Console.WriteLine();
}
MatrixGenerator.GenerateFile();
var tps = new RoadFounder();
tps.Main();
Console.WriteLine("---------------------------------\n");
foreach (var item in tps.Droga)
{
    Console.Write(item + " ");
}
//Console.WriteLine(tps.TabOcen);

