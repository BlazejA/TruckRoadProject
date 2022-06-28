using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

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
            MapGenerator map = new MapGenerator();

            var list = map.MapPointsGenerator();
            foreach (var item in list.Points)
            {
                map.CountShortestWays(list.Points, item);
            }

            for (int i = 1; i < map.ShortestRoads.Count; i++)
            {
                Line line = new Line();
                line.Visibility = Visibility.Visible;
                line.StrokeThickness = 1;
                line.Stroke = Brushes.Black;
                line.X1 = map.ShortestRoads[i - 1].X*2;
                line.Y1 = map.ShortestRoads[i - 1].Y * 2;
                line.X2 = map.ShortestRoads[i].X * 2;
                line.Y2 = map.ShortestRoads[i].Y * 2;
                Canvas.Children.Add(line);
                
            }
            foreach (var generatorShortestRoad in map.ShortestRoads)
            {
                text.Text += "X: " + generatorShortestRoad.X + " Y: " + generatorShortestRoad.Y + "\n";
            }
        }
    }
}
