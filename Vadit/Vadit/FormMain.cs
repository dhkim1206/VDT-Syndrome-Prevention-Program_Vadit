using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Vadit
{
    public partial class FormMain : Form
    {
        AppBase.FormManager _formManager;
        public FormCamera _formCamera;

        public VdtManager _vdtManager;
        PictureBox _pictureBox;


        public FormMain()
        {

            InitializeComponent();

            _formManager = new AppBase.FormManager(mainPanel);

            /*
            _formCamera = new FormCamera();
            _formCamera.TopLevel = false;
            _formCamera.FormBorderStyle = FormBorderStyle.None;
            _formCamera.Parent = mainPanel;
            _formCamera.Dock = DockStyle.Fill; // ���� �� ȭ��
            */
            //_formCameraThread.IsBackground = true; // �����带 ��׶���� �����Ͽ� ���α׷� ���� �� �Բ� ����ǵ��� �մϴ�.
            /*
            _isRunning = true;
            _cameraCapture = new VdtManager();
            */

            /*
            // ī�޶� ĸó ������ ����
            _captureThread = new Thread(CaptureThreadMethod);
            _captureThread.Start();
            */




            //_pictureBox = _formCamera.Controls.OfType<PictureBox>.FirstOrDefault();

        }
        private void btn_poseForm_Click(object sender, EventArgs e)
        {
            //������ ������ �����带 ������ ��
            _formManager.ChangeForm(typeof(FormCamera));
        }

        private void btn_ProgramExplain_Click(object sender, EventArgs e)
        {

        }

        private void btn_statisticsForm_Click(object sender, EventArgs e)
        {
            _formManager.ChangeForm(typeof(FormStatistics));
        }

        private void btn_FormSetting_Click(object sender, EventArgs e)
        {
            _formManager.ChangeForm(typeof(FormSetting));
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _vdtManager.Dispose();
        }
    }
}
