using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using TruckRoadProject.Models;
using TruckRoadProject.Models.TrucksModels;

namespace TruckRoadProject.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var generator = new MapGenerator();
            var map = generator.MapPointsGenerator();

            var matrix = MatrixGenerator.CreateMatrix(map);

            MatrixGenerator.GenerateFile();
            var tps = new RoadFounder();
            tps.Main();
            var truckList = new Queue<ITruck>();
            Random random = new Random();
            for (var i = 0; i < random.Next(3, 7); i++)
            {
                truckList.Enqueue(new BaseTruck());
            }

            var road = new Road(map, tps.Droga, truckList);

            TruckRide.Ride(road);
            map.Points.Add(map.Points[0]);
            tps.Droga.Add(tps.Droga[0]);
            for (var i = 1; i < tps.Droga.Count; i++)
            {
                var line = new Line();
                line.Visibility = Visibility.Visible;
                line.StrokeThickness = 1;
                line.Stroke = Brushes.Black;
                line.X1 = map.Points[tps.Droga[i - 1]].X * 2;
                line.Y1 = map.Points[tps.Droga[i - 1]].Y * 2;
                line.X2 = map.Points[tps.Droga[i]].X * 2;
                line.Y2 = map.Points[tps.Droga[i]].Y * 2;
                Canvas.Children.Add(line);

            }

            text.Text += road.Time + "\n";
        }
    }
}
