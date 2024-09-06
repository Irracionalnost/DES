using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KURS_ZD
{
    public partial class step4_1 : Form
    {
        bool exit = true;
        string content = string.Empty;
        int chipher_mode;
        int encode1_decode2 = 1;
        string restrict = string.Empty;
        bool default_restrict;
        public step4_1(string cont, int mode, string rest, bool scip)
        {
            InitializeComponent();
            content = cont;
            chipher_mode = mode;
            restrict = rest;
            default_restrict = scip;
            ActiveControl = passphrase;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.exit = false;
            SettingsPassForm workForm = new SettingsPassForm(content, chipher_mode);
            workForm.Show();
            
            Close();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            bool registr = restrict[0] == '1';
            bool digit = restrict[1] == '1';
            bool sym = restrict[2] == '1';
            int passlength = (restrict.Length == 4) ? (restrict[3] - '0') : (restrict[3]-'0') * 10 + (restrict[4] - '0');
            bool isOkeyrestrict = true;
            if (string.IsNullOrEmpty(passphrase.Text))
            {
                isOkeyrestrict = false;
                MessageBox.Show("Введите парольную фразу.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                if (!string.Equals(passphrase.Text, passphrase2.Text))
                {
                    isOkeyrestrict = false;
                    MessageBox.Show("Парольные фразы не совпадают.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }     
                else
                {
                    if (registr)
                    {
                        bool small = false;
                        bool big = false;
                        for (int i = 0; i < passphrase.TextLength && !(big && small); i++)
                            if (Char.IsUpper(passphrase.Text[i]))
                                big = true;
                            else
                                small = true;
                        if (!small || !big)
                        {
                            MessageBox.Show("Парольная фраза должна содержать символы верхнего и нижнего регистров!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            isOkeyrestrict = false;
                        }
                    }

                    if (digit && isOkeyrestrict)
                    {
                        bool d = false;
                        for (int i = 0; i < passphrase.TextLength && !d; i++)
                            if (Char.IsDigit(passphrase.Text[i]))
                                d = true;
                        if (!d)
                        {
                            MessageBox.Show("Парольная фраза должна содержать хотя бы одну цифру!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            isOkeyrestrict = false;
                        }
                    }

                    if (sym && isOkeyrestrict)
                    {
                        bool s = false;
                        char c;
                        for (int i = 0; i < passphrase.TextLength && !s; i++)
                        {
                            c = passphrase.Text[i];
                            if (c == '(' || c == '!' || c == '?' || c == '*' || c == ';' || c == '$' || c == '#' || c == '^' || c == '&' || c == ')' || c == '+' || c == '-')
                                s = true;
                        }
                        if (!s)
                        {
                            MessageBox.Show("Парольная фраза должна содержать специальные символы: (!?*;$#^&)+-", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            isOkeyrestrict = false;
                        }
                    }

                    if (passphrase.Text.Length < passlength && isOkeyrestrict)
                    {
                        isOkeyrestrict = false;
                        MessageBox.Show("Парольная фраза должна содержать хотя бы " + passlength.ToString() + " символов!", "Ошибка", MessageBoxButtons.OKCancel);
                    }
                }

            if (isOkeyrestrict)
            {
                step5 workForm = new step5(content, encode1_decode2, chipher_mode, passphrase.Text);
                workForm.Show();
                exit = false;
                Close();
            }         
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            GenerateForm generate = new GenerateForm(this, restrict, default_restrict);
            generate.Show();
        }

        private void step4_1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit)
                Application.Exit();
        }
    }
}
