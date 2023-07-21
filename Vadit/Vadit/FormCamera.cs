﻿using Emgu.CV;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;

namespace Vadit
{
    public partial class FormCamera : Form
    {
        private VdtManager _vdtManager;

        private bool _isRunning;

        BackgroundWorker _backgroundWorker;

        public FormCamera()
        {
            InitializeComponent();
            _vdtManager = new VdtManager();

            _isRunning = true;


            // BackgroundWorker 초기화 및 설정
            _backgroundWorker = new BackgroundWorker(); // 백그라운드 워커 객체 생성
            _backgroundWorker.WorkerReportsProgress = true; // 중간 보고 할거냐, 이걸 해줘야 중간보고를 할 수 있음
            _backgroundWorker.DoWork += new DoWorkEventHandler(OnDoWork); // 엔트리 포인트, 실행 할 함수를 매개변수로 줌
            _backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(OnProgressChanged); // 진행중인 진행 상활을 보고 받을거임
        }
        private void OnDoWork(object sender, DoWorkEventArgs e)
        {
            _vdtManager.CaptureFrameAndDetectSkeleton(_backgroundWorker);
        }

        private void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int curNum = (int)e.UserState; // ReportProgress 메서드에서 전달된 UserState를 int로 형변환하여 가져옴

            label1.Text = curNum.ToString(); // 여기서 우리는 라벨이 아닌 캡쳐박스를 보여줘야함
        }

        private void CaptureThreadMethod()
        {
            while (_isRunning)
            {
                _vdtManager.CaptureFrameAndDetectSkeleton();
                UpdatePictureBoxImage(_vdtManager.GetImage().ToBitmap());
                Thread.Sleep(3000); // 3초 대기
            }
        }

        private void UpdatePictureBoxImage(Image image)
        {
            if (pictureBox1.InvokeRequired)
                pictureBox1.Invoke(new MethodInvoker(() => UpdatePictureBoxImage(image)));
            else
                pictureBox1.Image = image;
        }

        private void FormCamera_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isRunning = false;
            _vdtManager.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
