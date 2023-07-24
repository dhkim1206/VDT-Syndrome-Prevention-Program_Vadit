﻿using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Vadit
{
    public partial class FormCamera : Form
    {
        private VdtManager _vdtManager;

        public FormCamera()
        {
            InitializeComponent();
            _vdtManager = new VdtManager(OnProgressing);

        }

        // 전달 받을 것들
        private void OnProgressing(object sender, ProgressChangedEventArgs e)
        {
            AnalyzeData obj = e.UserState as AnalyzeData;
            pictureBox1.Image = obj.AnalyzedImage;
            textBox1.Text = obj.Result;
        }

        private void FormCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            _vdtManager.Dispose();
        }
    }
}
