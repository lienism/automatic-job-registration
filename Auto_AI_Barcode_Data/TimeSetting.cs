using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;


namespace Auto_AI_Barcode_Data
{
    public partial class TimeSetting : Form
    {
        public TimeSetting()
        {
            InitializeComponent();
            this.Size = new Size(480, 400);
        }
        private void Time_start_Click(object sender, EventArgs e)
        {
            if (Schedule_name_setting_textbox.Text == "")
            {
                MessageBox.Show("Not Setting schedule name");
            }
            else
            {
                Schedule_create();
            }
        }
        public void Schedule_create()
        {
            openFileDialog1.ShowDialog();
            TaskService TS = new TaskService();
            TaskDefinition TD = TS.NewTask();
            TD.RegistrationInfo.Description = infor_textbox.Text;
            TD.Principal.UserId = string.Concat(Environment.UserDomainName, "\\", Environment.UserName); 
            TD.Principal.LogonType = TaskLogonType.InteractiveToken;
            DailyTrigger dt = new DailyTrigger();
            Double time = (Convert.ToDouble(process_time_hour_textbox.Text) * 3600) + (Convert.ToDouble(process_time_minutes_textbox.Text) * 60);
            dt.StartBoundary = DateTime.Today + TimeSpan.FromSeconds(time);
            dt.DaysInterval = Convert.ToInt16(kuri_textbox.Text);
            TD.Triggers.Add(dt);
            
            TD.Actions.Add(new ExecAction(openFileDialog1.FileName ,null,null));
            TS.RootFolder.RegisterTaskDefinition(Schedule_name_setting_textbox.Text, TD);
            MessageBox.Show("Success");
        }
        private void scheduledeletebutton_Click(object sender, EventArgs e)
        {
            if(Schedule_name_delete_textbox.Text == "")
            {
                MessageBox.Show("Not Setting schedule name");
            }
            else
            {
                Schedule_Delete();
            }
        }
        public void Schedule_Delete()
        {
            TaskService ts = new TaskService();
            Microsoft.Win32.TaskScheduler.Task t = ts.GetTask(Schedule_name_delete_textbox.Text); 
            ts.RootFolder.DeleteTask(Schedule_name_delete_textbox.Text);
            MessageBox.Show("Success");
        }

        private void taskschd_Click(object sender, EventArgs e)
        {
            String path = Environment.SystemDirectory + "/taskschd.msc";
            Process.Start(path);
        }
    }
}
