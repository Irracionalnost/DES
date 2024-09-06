using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KURS_ZD
{
    public partial class step3 : Form
    {
        public int encode1_decode2 = 1;
        public int chipher_mode = 1;
        public string content = string.Empty;
        bool exit = true;

        public step3(string cont, int encdec)
        {
            InitializeComponent();
            encode1_decode2 = encdec;
            content = cont;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            step2 workForm = new step2(content);
            workForm.Show();
            exit = false;
            Close();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (encode1_decode2 == 1)
            {
                SettingsPassForm workForm = new SettingsPassForm(content, chipher_mode);
                workForm.Show();
            }
            else
            {
                step4_2 workForm = new step4_2(content, chipher_mode);
                workForm.Show();
            }
            exit = false;
            Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            chipher_mode = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            chipher_mode = 2;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            chipher_mode = 3;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            chipher_mode = 4;
        }

        private void step3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit)
                Application.Exit();
        }
    }
}
