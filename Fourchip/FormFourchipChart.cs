using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Fourchip
{
    public partial class FormFourchipChart : Form
    {
        //
        //Form initialization
        //
        public FormFourchipChart(double[] tab, int tabIndex,int chartType)
        {
            int i = 0;
            
            InitializeComponent();

            //Setting the chart to visible
            chart.Visible = true;

            //Depending on the chart to be displayed ( Temp or Brightness )
            if (chartType == 1)
            {
                chart.ChartAreas[0].AxisY.Maximum = 50;
                chart.ChartAreas[0].AxisY.Minimum = -10;
                //Adding a link between all the values
                Series series = chart.Series.Add("Temperature ( C° )");
                //Setting the chart to a 3D Spline view
                series.ChartType = SeriesChartType.Spline;
                //Adding the first value of the tab to the chart ( prevent chart from being not displayed )
                series.Points.Add(tab[0]);
                //Adding values to the chart
                while (i < tabIndex)
                {
                    series.Points.Add(tab[i]);
                    i++;
                }
            }
            else
            {
                chart.ChartAreas[0].AxisY.Maximum = 100;
                chart.ChartAreas[0].AxisY.Minimum = 0;
                //Adding a link between all the values
                Series series = chart.Series.Add("Brightness ( % )");
                //Setting the chart to a 3D Spline view
                series.ChartType = SeriesChartType.Spline;
                //Adding the first value of the tab to the chart ( prevent chart from being not displayed )
                series.Points.Add(tab[0]);
                //Adding values to the chart
                while (i < tabIndex)
                {
                    series.Points.Add(tab[i]);
                    i++;
                }
            }
        }

        //
        //Performed action when the exit button is clicked.
        //
        private void FormFourchipChart_FormClosing(object sender, FormClosingEventArgs e)
        {
            chart.Dispose();
            this.Dispose();
        }
    }
}
