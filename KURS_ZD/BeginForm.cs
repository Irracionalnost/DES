using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KURS_ZD
{
    public partial class BeginForm : Form
    {
        public BeginForm()
        {
            InitializeComponent();
        }

        private void StartButton_MouseEnter(object sender, EventArgs e)
        {
            StartButton.ForeColor = System.Drawing.Color.FromArgb(245, 245, 90);
        }

        private void StartButton_MouseLeave(object sender, EventArgs e)
        {
            StartButton.ForeColor = SystemColors.ControlLight;
        }

        private void AboutButton_MouseEnter(object sender, EventArgs e)
        {
            AboutButton.ForeColor = System.Drawing.Color.FromArgb(245, 245, 90);
        }

        private void AboutButton_MouseLeave(object sender, EventArgs e)
        {
            AboutButton.ForeColor = SystemColors.ControlLight;
        }

        private void ExitButton_MouseEnter(object sender, EventArgs e)
        {
            ExitButton.ForeColor = System.Drawing.Color.FromArgb(245, 245, 90);
        }

        private void ExitButton_MouseLeave(object sender, EventArgs e)
        {
            ExitButton.ForeColor = SystemColors.ControlLight;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutButton_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.Show();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            step1 workForm = new step1();
            workForm.Show();
            this.Visible = false;
            this.Enabled = false;
        }
    }
}
