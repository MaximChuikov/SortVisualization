using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SortVisualization
{
    public partial class MainWindow : Window
    {
        public Line[] lines;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void ButtonBegin(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            uint[] arr = new uint[20];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = (uint)rand.Next(1, 101);
            Drawer.CreateLines(canvas, ref lines, arr);
            var alg = new SortAlgorithms((uint[] array, int e1, int e2) => Drawer.Swap(array, e1, e2, lines, canvas, slider.Value),
                                         () => TimeSpan.FromMilliseconds(slider.Value));
            alg.QuickSort(arr);
        }
        
        
        




        static class Drawer
        {
            public static void CreateLines(Canvas canvas, ref Line[] lines, uint[] arr)
            {
                double deltaY = canvas.ActualHeight / arr.Max();
                double deltaX = canvas.ActualWidth / (arr.Length - 1);
                lines = new Line[arr.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    lines[i] = new Line
                    {
                        Y2 = canvas.ActualHeight,
                        Y1 = canvas.ActualHeight - deltaY * arr[i],
                        Stroke = Brushes.White,
                        StrokeThickness = 4
                    };
                    lines[i].SetValue(LeftProperty, deltaX * i);
                }
                canvas.Children.Clear();
                foreach (var line in lines)
                {
                    canvas.Children.Add(line);
                }
            }
            public static void RedrawLine(Line sender, Line[] lines, Canvas canvas)
            {
                int index = -1;
                for (int i = 0; i < lines.Length; i++)
                    if (sender == lines[i])
                        index = i;
                if (index == -1)
                    throw new Exception();
                double deltaX = canvas.ActualWidth / (lines.Length - 1);
                lines[index].SetValue(LeftProperty, deltaX * index);
            }
            public static void RedrawLines(Line[] lines, Canvas canvas)
            {
                double deltaX = canvas.ActualWidth / (lines.Length - 1);
                for (int i = 0; i < lines.Length; i++)
                    lines[i].SetValue(LeftProperty, deltaX * i);
            }
            public static void Swap(uint[] arr, int e1, int e2, Line[] lines, Canvas canvas, double sliderValue)
            {
                var temp = arr[e1];
                arr[e1] = arr[e2];
                arr[e2] = temp;
                var tempLine = lines[e1];
                lines[e1] = lines[e2];
                lines[e2] = tempLine;
                SwapAnimation(e2, e1, lines, sliderValue, canvas);
            }
            static void SwapAnimation(int e1, int e2, Line[] lines, double sliderValue, Canvas canvas)
            {
                var time = TimeSpan.FromMilliseconds(sliderValue);
                var animE1 = new DoubleAnimation((double)lines[e1].GetValue(LeftProperty), (double)lines[e2].GetValue(LeftProperty), time, FillBehavior.Stop);
                var animE2 = new DoubleAnimation((double)lines[e2].GetValue(LeftProperty), (double)lines[e1].GetValue(LeftProperty), time, FillBehavior.Stop);
                animE1.Completed += (s, _) => RedrawLine(lines[e1], lines, canvas);
                animE2.Completed += (s, _) => RedrawLine(lines[e2], lines, canvas);
                lines[e1].BeginAnimation(LeftProperty, animE1);
                lines[e2].BeginAnimation(LeftProperty, animE2);
            }
        }
    }
    
}