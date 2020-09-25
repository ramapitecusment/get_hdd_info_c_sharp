using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Management;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ManagementObjectSearcher mosDisks = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pyramid;
            chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
        }

        string[] HDDinfo = new string[4];

        int totalSize;
        int availableSize;
        int leftSize, i;

        private void diskInfo()
        {
            i = 0; 
            foreach (var drive in DriveInfo.GetDrives())
            {
                dataGridView1.Rows.Add();
                HDDinfo[0] = drive.Name;
                totalSize = (int) Math.Round((Convert.ToDouble(drive.TotalSize / 1024 / 1024 / 1024)), 2);
                availableSize = (int) Math.Round((Convert.ToDouble(drive.TotalFreeSpace / 1024 / 1024 / 1024)), 2);
                leftSize = totalSize - availableSize;
                HDDinfo[1] = totalSize.ToString();
                HDDinfo[2] = availableSize.ToString();
                HDDinfo[3] = leftSize.ToString();

                dataGridView1.Rows[i].Cells[0].Value = HDDinfo[0];
                dataGridView1.Rows[i].Cells[1].Value = HDDinfo[1];
                dataGridView1.Rows[i].Cells[2].Value = HDDinfo[2];
                dataGridView1.Rows[i].Cells[3].Value = HDDinfo[3];
                i++;
            }
        }

        private void clearChart(System.Windows.Forms.DataVisualization.Charting.Chart chart)
        {
            foreach (var series in chart.Series)
            {
                series.Points.Clear();
            }
        }

        private void графикиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clearChart(chart1);
            clearChart(chart2);
            clearChart(chart3);
            chart1.Series[0].Points.AddXY(HDDinfo[1], HDDinfo[1]);
            chart1.Series[0].Points.AddXY(HDDinfo[2], HDDinfo[2]);
            chart1.Series[0].Points.AddXY(HDDinfo[3], HDDinfo[3]);
            chart2.Series[0].Points.AddXY(HDDinfo[1], HDDinfo[1]);
            chart2.Series[0].Points.AddXY(HDDinfo[2], HDDinfo[2]);
            chart2.Series[0].Points.AddXY(HDDinfo[3], HDDinfo[3]);
            chart3.Series[0].Points.AddXY(HDDinfo[1], HDDinfo[1]);
            chart3.Series[0].Points.AddXY(HDDinfo[2], HDDinfo[2]);
            chart3.Series[0].Points.AddXY(HDDinfo[3], HDDinfo[3]);
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {

            chart1.Series.Clear();
            chart2.Series.Clear();
            chart3.Series.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void выполнитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            diskInfo();

        }
    }
}
