﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;

namespace Vadit
{
    public partial class FormStatistics : Form
    {
        private ChartManager _chartManager;
        private DashBoardManager _flowPanelManager;

        public FormStatistics()
        {
            InitializeComponent();
            _flowPanelManager = new DashBoardManager(pn_Nodata, PictureFlowLayout, DateTime.Now.Date, label4, label1, label2, label3);
            _chartManager = new ChartManager(_flowPanelManager);
        }

        private void FormStatistics_Load(object sender, EventArgs e)
        {
            _chartManager.LoadChartData();
            _chartManager.BindChartData(chart1);
        }

    }
}
