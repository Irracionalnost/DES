
namespace KURS_ZD
{
    partial class SettingsPassForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsPassForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.nextButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.setButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lengthpass = new System.Windows.Forms.DomainUpDown();
            this.checkBoxSym = new System.Windows.Forms.CheckBox();
            this.checkBoxDigit = new System.Windows.Forms.CheckBox();
            this.checkBoxRegistr = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nextButton);
            this.panel1.Controls.Add(this.backButton);
            this.panel1.Controls.Add(this.setButton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lengthpass);
            this.panel1.Controls.Add(this.checkBoxSym);
            this.panel1.Controls.Add(this.checkBoxDigit);
            this.panel1.Controls.Add(this.checkBoxRegistr);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(748, 587);
            this.panel1.TabIndex = 1;
            // 
            // nextButton
            // 
            this.nextButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.nextButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.nextButton.Location = new System.Drawing.Point(507, 491);
            this.nextButton.Name = "nextButton";
            this.nextButton.Size = new System.Drawing.Size(157, 69);
            this.nextButton.TabIndex = 9;
            this.nextButton.Text = "Пропустить";
            this.nextButton.UseVisualStyleBackColor = true;
            this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
            // 
            // backButton
            // 
            this.backButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.backButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.backButton.Location = new System.Drawing.Point(317, 491);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(157, 69);
            this.backButton.TabIndex = 8;
            this.backButton.Text = "Назад";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // setButton
            // 
            this.setButton.BackColor = System.Drawing.Color.White;
            this.setButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(228)))), ((int)(((byte)(176)))));
            this.setButton.FlatAppearance.BorderSize = 3;
            this.setButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.setButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.setButton.Location = new System.Drawing.Point(73, 386);
            this.setButton.Name = "setButton";
            this.setButton.Size = new System.Drawing.Size(591, 70);
            this.setButton.TabIndex = 6;
            this.setButton.Text = "Установить выбранные параметры и продолжить\r\n\r\n";
            this.setButton.UseVisualStyleBackColor = false;
            this.setButton.Click += new System.EventHandler(this.setButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(228)))), ((int)(((byte)(156)))));
            this.label2.Location = new System.Drawing.Point(73, 304);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(355, 28);
            this.label2.TabIndex = 5;
            this.label2.Text = "Длина парольной фразы:";
            // 
            // lengthpass
            // 
            this.lengthpass.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lengthpass.Items.Add("30");
            this.lengthpass.Items.Add("29");
            this.lengthpass.Items.Add("28");
            this.lengthpass.Items.Add("27");
            this.lengthpass.Items.Add("26");
            this.lengthpass.Items.Add("25");
            this.lengthpass.Items.Add("24");
            this.lengthpass.Items.Add("23");
            this.lengthpass.Items.Add("22");
            this.lengthpass.Items.Add("21");
            this.lengthpass.Items.Add("20");
            this.lengthpass.Items.Add("19");
            this.lengthpass.Items.Add("18");
            this.lengthpass.Items.Add("17");
            this.lengthpass.Items.Add("16");
            this.lengthpass.Items.Add("15");
            this.lengthpass.Items.Add("14");
            this.lengthpass.Items.Add("13");
            this.lengthpass.Items.Add("12");
            this.lengthpass.Items.Add("11");
            this.lengthpass.Items.Add("10");
            this.lengthpass.Items.Add("9");
            this.lengthpass.Items.Add("8");
            this.lengthpass.Items.Add("7");
            this.lengthpass.Items.Add("6");
            this.lengthpass.Items.Add("5");
            this.lengthpass.Items.Add("4");
            this.lengthpass.Location = new System.Drawing.Point(507, 297);
            this.lengthpass.Name = "lengthpass";
            this.lengthpass.ReadOnly = true;
            this.lengthpass.Size = new System.Drawing.Size(157, 35);
            this.lengthpass.TabIndex = 4;
            this.lengthpass.Text = "8";
            // 
            // checkBoxSym
            // 
            this.checkBoxSym.AutoSize = true;
            this.checkBoxSym.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxSym.Location = new System.Drawing.Point(73, 246);
            this.checkBoxSym.Name = "checkBoxSym";
            this.checkBoxSym.Size = new System.Drawing.Size(516, 32);
            this.checkBoxSym.TabIndex = 3;
            this.checkBoxSym.Text = "Использовать символы (!?*;$#^&)+-";
            this.checkBoxSym.UseVisualStyleBackColor = true;
            // 
            // checkBoxDigit
            // 
            this.checkBoxDigit.AutoSize = true;
            this.checkBoxDigit.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxDigit.Location = new System.Drawing.Point(73, 190);
            this.checkBoxDigit.Name = "checkBoxDigit";
            this.checkBoxDigit.Size = new System.Drawing.Size(321, 32);
            this.checkBoxDigit.TabIndex = 2;
            this.checkBoxDigit.Text = "Использовать цифры";
            this.checkBoxDigit.UseVisualStyleBackColor = true;
            // 
            // checkBoxRegistr
            // 
            this.checkBoxRegistr.AutoSize = true;
            this.checkBoxRegistr.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxRegistr.Location = new System.Drawing.Point(73, 133);
            this.checkBoxRegistr.Name = "checkBoxRegistr";
            this.checkBoxRegistr.Size = new System.Drawing.Size(591, 32);
            this.checkBoxRegistr.TabIndex = 1;
            this.checkBoxRegistr.Text = "Использовать верхний и нижний регистр";
            this.checkBoxRegistr.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(228)))), ((int)(((byte)(156)))));
            this.label1.Location = new System.Drawing.Point(73, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(640, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Задайте параметры парольной фразы";
            // 
            // SettingsPassForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(30F, 52F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(746, 581);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(11, 8, 11, 8);
            this.Name = "SettingsPassForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Параметры";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsPassForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button nextButton;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button setButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DomainUpDown lengthpass;
        private System.Windows.Forms.CheckBox checkBoxSym;
        private System.Windows.Forms.CheckBox checkBoxDigit;
        private System.Windows.Forms.CheckBox checkBoxRegistr;
        private System.Windows.Forms.Label label1;
    }
}