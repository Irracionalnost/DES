using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KURS_ZD
{
    public partial class step2 : Form
    {
        public int encode1_decode2 = 1;
        public string content = string.Empty;
        bool exit = true;
        public step2(string content)
        {
            InitializeComponent();
            this.content = content;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            step1 workForm = new step1();
            workForm.Show();
            exit = false;
            Close();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            step3 workForm = new step3(content, encode1_decode2);
            workForm.Show();
            exit = false;
            Close();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            radioButton2.Checked = false;
            radioButton1.Checked = true;
            encode1_decode2 = 1;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = true;
            encode1_decode2 = 2;
        }

        private void step2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit)
                Application.Exit();
        }
    }
}
