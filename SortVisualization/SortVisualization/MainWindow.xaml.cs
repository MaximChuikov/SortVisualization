using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SortVisualization
{
    public partial class MainWindow : Window
    {
        public Line[] lines = Array.Empty<Line>();
        public ObservableCollection<LogLine> LogLines { get; set; } = new ObservableCollection<LogLine>();
        public MainWindow()
        {
            InitializeComponent();
            logBox.ItemsSource = LogLines;
        }

        private void ButtonBegin(object sender, RoutedEventArgs e)
        {
            LogLines.Clear();
            int arrLenght = int.Parse(lenght.Text.Length != 0 ? lenght.Text : "10");
            uint[] arr = @case.SelectedIndex switch
            {
                1 => InverseFilling(arrLenght),
                _ => RandomFilling(arrLenght)
            };
            Drawer.CreateLines(canvas, ref lines, arr);
            var alg = new SortAlgorithms((uint[] array, int e1, int e2) => Drawer.Swap(array, e1, e2, lines, canvas, slider.Value),
                                         () => TimeSpan.FromMilliseconds(slider.Value),
                                         (string f, string s, string t) => 
                                         {
                                             LogLines.Add(new LogLine(f, s, t));
                                             scrollViewer.ScrollToEnd();
                                         });
            switch (method.SelectedIndex)
            {
                case 0:
                    alg.ShellSort(arr);
                    break;
                case 1:
                    alg.ShellSort(arr);
                    break;
                default:
                    return;
            }
        }
        private uint[] RandomFilling(int lenght)
        {
            Random rand = new Random();
            var arr = new uint[lenght];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = (uint)rand.Next(1, 101);
            return arr;
        }
        private uint[] InverseFilling(int lenght)
        {
            var arr = new uint[lenght];
            for (int i = 0; i < arr.Length; i++)
                arr[i] = (uint)(lenght - i + 1);
            return arr;
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.Key is not (>= 34 and <= 43) and not (>= 74 and <= 83))
                e.Handled = true;
        }
        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e) => Drawer.RedrawLines(lines, canvas);
        public class LogLine
        {
            public string FirstPhase { get; set; }
            public string SecondPhase { get; set; }
            public string ThirdPhase { get; set; }
            public LogLine(string first, string second, string third)
            {
                FirstPhase = first;
                SecondPhase = second;
                ThirdPhase = third;
            }
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