using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reminder_App
{
    public partial class ReminderApp : Form
    {
        public ReminderApp()
        {
            InitializeComponent();
        }
        struct stEvent
        {
            public string EventName;
            public DateTime EventDate;
        }

        List<stEvent> Events = new List<stEvent>();

        void AddEventToList()
        {
            listBox1.Items.Add(txtEventName.Text);

            stEvent sEvent;
            sEvent.EventName = txtEventName.Text;
            sEvent.EventDate = dateTimePicker1.Value;

            Events.Add(sEvent);
            txtEventName.Text = "";
            txtEventName.Focus();
        }

        void DeleteSelected()
        {
         
            int IndexToDelete = listBox1.SelectedIndex;

            if (IndexToDelete >= 0)
            {
                listBox1.Items.RemoveAt(IndexToDelete);
                Events.RemoveAt(IndexToDelete);
                txtEventName.Focus();
            }
        }

        bool IsEventNameFull()
        {
            return !string.IsNullOrEmpty(txtEventName.Text);
        }
        bool IsDatetimeValid()
        {
            return dateTimePicker1.Value >= DateTime.Now;
        }

        bool CheckInfo()
        {
            if(!IsEventNameFull())
            {
                MessageBox.Show("Enter an Event Name");
                txtEventName.Focus();
                return false;
            }    

            if(!IsDatetimeValid())
            {
                MessageBox.Show("Please Enter a Valid Infomation");
                return false;
            }
            return true;
        }
        void Reset()
        {
            listBox1.Items.Clear();
            Events.Clear();
            timer1.Stop();
        }
        
        void ShowNotifyIcon(int Itemindex)
        {
            notifyIcon1.Icon = SystemIcons.Information;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipTitle = "Reminder";
            notifyIcon1.BalloonTipText = $"It's Time to {Events[Itemindex].EventName}";
            notifyIcon1.ShowBalloonTip(5000);
        }

        private void txtEventName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }


        private void btnAddReminder_Click(object sender, EventArgs e)
        {
            if(!CheckInfo())return;

            AddEventToList();
            timer1.Start();
        }

     
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            for (int i = 0; i < Events.Count; i++)
            {
                if (DateTime.Now >= Events[i].EventDate)
                {
                    ShowNotifyIcon(i);
                    listBox1.Items.RemoveAt(i);
                    Events.RemoveAt(i);

                }
            }
        }

        private void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            DeleteSelected();

        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            Reset();
        }
    }
}
