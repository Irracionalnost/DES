using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KURS_ZD
{
    public partial class step4_2 : Form
    {
        string content = string.Empty;
        int chipher_mode;
        int encode1_decode2 = 2;
        bool exit = true;
        public step4_2(string cont, int mode)
        {
            InitializeComponent();
            content = cont;
            chipher_mode = mode;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            exit = false;
            step3 workForm = new step3(content, encode1_decode2);
            workForm.Show();
            Close();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DataBox.Text))
                MessageBox.Show("Введите парольную фразу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                exit = false;
                step5 workForm = new step5(content, encode1_decode2, chipher_mode, DataBox.Text);
                workForm.Show();
                Close();
            }   
        }

        private void step4_2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit)
                Application.Exit();
        }

        private void step4_2_Load(object sender, EventArgs e)
        {
            ActiveControl = DataBox;
        }
    }
}
