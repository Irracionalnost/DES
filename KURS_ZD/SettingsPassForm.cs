using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KURS_ZD
{
    public partial class SettingsPassForm : Form
    {
        bool exit = true;
        string content;
        string restrict = string.Empty;
        int chipher_mode;
        public SettingsPassForm(string cont, int mode)
        {
            InitializeComponent();
            content = cont;
            chipher_mode = mode;
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            exit = false;
            restrict = "0008";
            step4_1 workForm = new step4_1(content, chipher_mode, restrict, true);
            workForm.Show();
            Close();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            step3 workForm = new step3(content, 1);
            workForm.Show();
            exit = false;
            Close();
        }

        private void setButton_Click(object sender, EventArgs e)
        {
            restrict += (checkBoxRegistr.Checked) ? '1' : '0';
            restrict += (checkBoxDigit.Checked) ? '1' : '0';
            restrict += (checkBoxSym.Checked) ? '1' : '0';
            restrict += lengthpass.Text;
            step4_1 workForm = new step4_1(content, chipher_mode, restrict, false);
            workForm.Show();
            exit = false;
            Close();
        }

        private void SettingsPassForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit)
                Application.Exit();
        }
    }
}
