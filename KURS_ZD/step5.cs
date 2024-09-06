using Program_DES;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace KURS_ZD
{
    public partial class step5 : Form
    {
        string content = string.Empty;
        int encode1_decode2;
        int chipher_mode;
        string passphrase = string.Empty;
        bool exit = true;
        public step5(string cont, int encdec, int mode, string pass)
        {
            InitializeComponent();
            content = cont;
            encode1_decode2 = encdec;
            chipher_mode = mode;
            passphrase = pass;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            DES_CRYPT des = new DES_CRYPT(content, chipher_mode, passphrase);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (encode1_decode2 == 1)
                {
                    des.Des_Encode(saveFileDialog1.FileName);
                    pictureBox1.Visible = true;
                }
                    
                else
                   if (des.Des_Decode(content, saveFileDialog1.FileName))
                   {
                        pictureBox1.Visible = true;
                        label2.Visible = false;
                        label3.Visible = false;
                   }
                   else
                   {
                    pictureBox1.Visible = true;
                    pictureBox1.Image = Image.FromFile("error_status.bmp");
                    label2.Visible = true;
                    label3.Visible = true;
                }
            }
            if (delinpfilecheckbox.Checked && File.Exists(content))
                File.Delete(content);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void OpenStep1Button_Click(object sender, EventArgs e)
        {
            step1 workForm = new step1();
            workForm.Show();
            exit = false;
            Close();
        }

        private void step5_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit)
                Application.Exit();
        }

        private void step5_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            label2.Visible = false;
            
        }

    }
}
