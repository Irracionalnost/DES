using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KURS_ZD
{
    public partial class GenerateForm : Form
    {
        string restrict = string.Empty;
        bool restregistr = false;
        bool restdigit = false;
        bool restsym = false;
        int restpasslen = 0;
        string generate_pass = string.Empty;
        step4_1 parent_form;
        public GenerateForm(step4_1 form, string rest, bool defaul_restrict)
        {
            InitializeComponent();
            restrict = rest;
            if (!defaul_restrict)
            {
                mes1.Visible = true;
                mes2.Visible = true;
                restregistr = rest[0] == '1';
                checkBoxRegistr.Checked = restrict[0] == '1';
                restdigit = rest[1] == '1';
                checkBoxDigit.Checked = restrict[1] == '1';
                restsym = rest[2] == '1';
                checkBoxSym.Checked = restrict[2] == '1';
                restpasslen = (rest.Length == 4) ? (rest[3] - '0') : (rest[3] - '0') * 10 + (rest[4] - '0');
                passlength.Text = restpasslen.ToString();
            }
            parent_form = form;
        }

        private void checkBoxRegistr_CheckedChanged(object sender, EventArgs e)
        {
            if (restregistr)
                checkBoxRegistr.Checked = restrict[0] == '1';

        }

        private void checkBoxDigit_CheckedChanged(object sender, EventArgs e)
        {
            if (restdigit)
                checkBoxDigit.Checked = restrict[1] == '1';
        }

        private void checkBoxSym_CheckedChanged(object sender, EventArgs e)
        {
            if (restsym)
                checkBoxSym.Checked = restrict[2] == '1';
        }

        private void passlength_SelectedItemChanged(object sender, EventArgs e)
        {
            
            if (restpasslen != 0)
                if (Int32.Parse(passlength.Text) < restpasslen)
                    passlength.Text = restpasslen.ToString();
        }

        private void generatebutton_Click(object sender, EventArgs e)
        {
            generate_pass = string.Empty;
            Random rnd = new Random();

            char small_sym='0';
            char BIG_SYM='0';
            if (checkBoxRegistr.Checked)
            {
                BIG_SYM = (char)rnd.Next(65, 90);
                small_sym = (char)rnd.Next(97, 122);
            }

            int d = 0; ;
            if (checkBoxDigit.Checked)
                d = rnd.Next(0, 9);
            
                
            char s='0';
            if (checkBoxSym.Checked)
            {
                char[] syms = { '(', '!', '?', '*', ';', '$', '#', '^', ')', '+', '-' };
                s = syms[rnd.Next(0, 11)];
            }

            //определяем позицию обязательных символов
            int[] randpos_restrict_sym = new int[4];
            if (!checkBoxRegistr.Checked)
            {
                randpos_restrict_sym[0] = -1;
                randpos_restrict_sym[1] = -1;
            }
            if (!checkBoxDigit.Checked)
                randpos_restrict_sym[2] = -1;
            if (!checkBoxSym.Checked)
                randpos_restrict_sym[3] = - 1;
            List<int> randpos = new List<int>();
            int cur_pos;
            for (int i = 0; i < 4; i++)
            {
                if (randpos_restrict_sym[i] != -1)
                {
                    cur_pos = rnd.Next(Int32.Parse(passlength.Text));
                    while (randpos.Contains(cur_pos))
                        cur_pos = rnd.Next(Int32.Parse(passlength.Text));
                    randpos_restrict_sym[i] = cur_pos;
                }               
            }


            bool flag;
            for (int i = 0; i < Int32.Parse(passlength.Text); i++)
            {
                flag = false;
                for (int j = 0; j < 4; j++)
                    if (i == randpos_restrict_sym[j])
                    {
                        flag = true;
                        if (j == 0)
                            generate_pass += small_sym;
                        if (j == 1)
                            generate_pass += BIG_SYM;
                        if (j == 2)
                            generate_pass += d.ToString();
                        if (j == 3)
                            generate_pass += s;
                    }
                if (!flag)
                    generate_pass += (char)rnd.Next(33, 127);
            }
            passtextbox.Text = generate_pass;
        }

        private void checkBoxshow_CheckedChanged(object sender, EventArgs e)
        {
            passtextbox.UseSystemPasswordChar = !passtextbox.UseSystemPasswordChar;
        }

        private void nextbutton_Click(object sender, EventArgs e)
        {
            parent_form.passphrase.Text = generate_pass;
            parent_form.passphrase2.Text = generate_pass;
            parent_form.passphrase.UseSystemPasswordChar = true;
            parent_form.passphrase2.UseSystemPasswordChar = true;
            Close();
        }

        private void backButton_Click_1(object sender, EventArgs e)
        {
            Close();
        }

    }
}
