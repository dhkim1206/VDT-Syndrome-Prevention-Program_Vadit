
using Emgu.CV.Ocl;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Threading;
using System.Windows.Forms;
using Vadit.Properties;
using static Vadit.AppBase;
using Timer = System.Threading.Timer;
namespace Vadit
{
    public partial class FormMain : Form
    {
        AppBase.FormManager _formManager;
        private NotifyIcon notifyIcon;
        FormPopUp _formPopUp;
        public VdtManager _vdtManager;

        //��ǥ����
        int Current_Y;
        //��������
        int Target_Y;
        //�����̵� �ӵ� ����
        int STEP_SLIDING;

        public FormMain()
        {
            InitializeComponent();

            // Ʈ���� ������ �ʱ�ȭ
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Properties.Resources.Vadit_Icon;
            notifyIcon.Text = "Vadit";
            notifyIcon.Visible = true;

            // Ʈ���� �������� Ŭ���ϸ� ���� ���̰� ��
            notifyIcon.MouseClick += NotifyIcon_MouseClick;


            _formManager = new AppBase.FormManager(mainPanel);
            AppBase.AppConf = new AppConfig("data.xml");
            _formPopUp = new FormPopUp();
            AppGlobal.StartTimer();

            AppBase.AppConf = new AppConfig("data.xml");

            _formPopUp = new FormPopUp();

        }

        private void timerSliding_Tick(object sender, EventArgs e)
        {
            if (Current_Y < Target_Y)
            {
                int y = Current_Y += STEP_SLIDING;
                pn_Scroll.Location = new Point(pn_Scroll.Location.X, y);
                if (y >= Target_Y)
                {
                    timerSliding.Stop();
                }
            }
            else
            {
                int y = Current_Y -= STEP_SLIDING;
                pn_Scroll.Location = new Point(pn_Scroll.Location.X, y);
                if (y <= Target_Y)
                {
                    timerSliding.Stop();
                }
            }
            if (STEP_SLIDING > 59)
            {
                STEP_SLIDING -= 30;
            }
        }
        public void StartDetect()
        {
            AppGlobal.VM = new VdtManager(OnProgressing);
            AppGlobal.VM._bgw.RunWorkerAsync();
        }
        private void OnProgressing(object sender, ProgressChangedEventArgs e)
        {
            AnalyzeData obj = e.UserState as AnalyzeData;
        }


        private async void btn_statisticsForm_Click(object sender, EventArgs e)
        {
            /*
            Current_Y = pn_Scroll.Location.Y;
            Target_Y = btn_statisticsForm.Location.Y + 10;
            STEP_SLIDING = Math.Abs(Current_Y - Target_Y) / 64 * 20;
            Debug.WriteLine(Math.Abs(Current_Y - Target_Y));
            Debug.WriteLine(STEP_SLIDING);
            timerSliding.Start();

            _formManager.ChangeForm(typeof(FormStatistics));
            */
            {

                pn_Scroll.Location = new Point(pn_Scroll.Location.X, btn_statisticsForm.Location.Y + 20);
                pn_Scroll.Height = 30;
                await Task.Delay(80); // �ִϸ��̼� ����

                /*
                pn_Scroll.Location = new Point(pn_Scroll.Location.X, btn_statisticsForm.Location.Y + 15);
                pn_Scroll.Height = 45;
                await Task.Delay(70); // �ִϸ��̼� ����
                */
                pn_Scroll.Location = new Point(pn_Scroll.Location.X, btn_statisticsForm.Location.Y + 10);
                pn_Scroll.Height = 60;
            }
        }

        private async void btn_FormSetting_Click(object sender, EventArgs e)
        {
            Current_Y = pn_Scroll.Location.Y;
            Target_Y = btn_FormSetting.Location.Y + 10;
            STEP_SLIDING = Math.Abs(Current_Y - Target_Y) / 64 * 20;
            timerSliding.Start();

            _formManager.ChangeForm(typeof(FormSetting));

        }

        private async void btn_ProgramExplain_Click_1(object sender, EventArgs e)
        {
            Current_Y = pn_Scroll.Location.Y;
            Target_Y = btn_ProgramExplain.Location.Y + 10;
            STEP_SLIDING = Math.Abs(Current_Y - Target_Y) / 64 * 20;
            timerSliding.Start();
        }


        private void btn_producer_Click(object sender, EventArgs e)
        {
            Current_Y = pn_Scroll.Location.Y;
            Target_Y = btn_producer.Location.Y + 10;
            STEP_SLIDING = Math.Abs(Current_Y - Target_Y) / 64 * 20;
            timerSliding.Start();
        }
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formManager.CloseCurrentForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _formPopUp.Show();
        }

        private void btn_end_Click(object sender, EventArgs e)
        {
            // ���� �޴� ������ Ŭ�� �� ���α׷� ����
            notifyIcon.Dispose();
            this.Dispose();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            // ����ڰ� ���� �� �� ����� Ʈ���� ������ ǥ��
            this.Hide();
        }
        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Ʈ���� ������ Ŭ�� �� ���� ���̰� ��
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }


    }
}
