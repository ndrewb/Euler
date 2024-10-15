using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Euler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupChart();
        }

        // Метод для настройки графика
        private void SetupChart()
        {
            cartesianChart1.Series = new SeriesCollection
    {
        new LineSeries
        {
            Values = new ChartValues<double>()
        }
    };

            // Настройка оси X
            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "x",
                MinValue = 0,  
                MaxValue = 1,  
                LabelFormatter = value => value.ToString("F2") 
            });

            // Настройка оси Y
            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "y",
                LabelFormatter = value => value.ToString("F2") 
            });
        }


        // Кнопка для запуска вычислений
        private void button1_Click(object sender, EventArgs e)
        {
            double x0 = 0;
            double y0 = 1;
            double h = 0.1;
            double xEnd = 1;

            var results = SolveEuler(x0, y0, h, xEnd);

            dataGridView1.Rows.Clear();
            cartesianChart1.Series[0].Values.Clear();

            ChartValues<LiveCharts.Defaults.ObservablePoint> points = new ChartValues<LiveCharts.Defaults.ObservablePoint>();

            foreach (var (x, y) in results)
            {
                dataGridView1.Rows.Add(x.ToString("F2"), y.ToString("F4"));

                points.Add(new LiveCharts.Defaults.ObservablePoint(x, y));
            }

            cartesianChart1.Series[0].Values = points;
        }


        // Функция для вычисления производной
        private double Derivative(double x, double y)
        {
            return x * x - 2 * y;
        }

        // Метод Эйлера для решения ОДУ
        private List<(double, double)> SolveEuler(double x0, double y0, double h, double xEnd)
        {
            List<(double, double)> results = new List<(double, double)>();
            double x = x0;
            double y = y0;
            double tolerance = 1e-10; // Погрешность для сравнения double

            results.Add((x, y));

            while (Math.Abs(x - xEnd) > tolerance) 
            {
                y = y + h * Derivative(x, y);
                x = x + h;
                results.Add((x, y));
            }

            return results;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
