
namespace KURS_ZD
{
    partial class GenerateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerateForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBoxshow = new System.Windows.Forms.CheckBox();
            this.passtextbox = new System.Windows.Forms.TextBox();
            this.nextbutton = new System.Windows.Forms.Button();
            this.mes2 = new System.Windows.Forms.Label();
            this.mes1 = new System.Windows.Forms.Label();
            this.backButton = new System.Windows.Forms.Button();
            this.generatebutton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.passlength = new System.Windows.Forms.DomainUpDown();
            this.checkBoxSym = new System.Windows.Forms.CheckBox();
            this.checkBoxDigit = new System.Windows.Forms.CheckBox();
            this.checkBoxRegistr = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBoxshow);
            this.panel2.Controls.Add(this.passtextbox);
            this.panel2.Controls.Add(this.nextbutton);
            this.panel2.Controls.Add(this.mes2);
            this.panel2.Controls.Add(this.mes1);
            this.panel2.Controls.Add(this.backButton);
            this.panel2.Controls.Add(this.generatebutton);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.passlength);
            this.panel2.Controls.Add(this.checkBoxSym);
            this.panel2.Controls.Add(this.checkBoxDigit);
            this.panel2.Controls.Add(this.checkBoxRegistr);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(0, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(748, 587);
            this.panel2.TabIndex = 2;
            // 
            // checkBoxshow
            // 
            this.checkBoxshow.AutoSize = true;
            this.checkBoxshow.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxshow.Location = new System.Drawing.Point(521, 399);
            this.checkBoxshow.Name = "checkBoxshow";
            this.checkBoxshow.Size = new System.Drawing.Size(147, 29);
            this.checkBoxshow.TabIndex = 15;
            this.checkBoxshow.Text = "Показать ";
            this.checkBoxshow.UseVisualStyleBackColor = true;
            this.checkBoxshow.CheckedChanged += new System.EventHandler(this.checkBoxshow_CheckedChanged);
            // 
            // passtextbox
            // 
            this.passtextbox.Font = new System.Drawing.Font("Verdana", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.passtextbox.Location = new System.Drawing.Point(73, 396);
            this.passtextbox.Name = "passtextbox";
            this.passtextbox.Size = new System.Drawing.Size(416, 35);
            this.passtextbox.TabIndex = 14;
            this.passtextbox.UseSystemPasswordChar = true;
            // 
            // nextbutton
            // 
            this.nextbutton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.nextbutton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.nextbutton.Location = new System.Drawing.Point(521, 470);
            this.nextbutton.Name = "nextbutton";
            this.nextbutton.Size = new System.Drawing.Size(192, 78);
            this.nextbutton.TabIndex = 13;
            this.nextbutton.Text = "Продолжить";
            this.nextbutton.UseVisualStyleBackColor = true;
            this.nextbutton.Click += new System.EventHandler(this.nextbutton_Click);
            // 
            // mes2
            // 
            this.mes2.AutoSize = true;
            this.mes2.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.mes2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.mes2.Location = new System.Drawing.Point(362, 102);
            this.mes2.Name = "mes2";
            this.mes2.Size = new System.Drawing.Size(362, 16);
            this.mes2.TabIndex = 12;
            this.mes2.Text = "заданы с учетом установленных ограничений)";
            this.mes2.Visible = false;
            // 
            // mes1
            // 
            this.mes1.AutoSize = true;
            this.mes1.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.mes1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.mes1.Location = new System.Drawing.Point(50, 102);
            this.mes1.Name = "mes1";
            this.mes1.Size = new System.Drawing.Size(306, 16);
            this.mes1.TabIndex = 11;
            this.mes1.Text = "(параметры, которые нельзя изменить,";
            this.mes1.Visible = false;
            // 
            // backButton
            // 
            this.backButton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.backButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.backButton.Location = new System.Drawing.Point(73, 470);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(192, 81);
            this.backButton.TabIndex = 10;
            this.backButton.Text = "Назад";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click_1);
            // 
            // generatebutton
            // 
            this.generatebutton.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.generatebutton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.generatebutton.Location = new System.Drawing.Point(297, 470);
            this.generatebutton.Name = "generatebutton";
            this.generatebutton.Size = new System.Drawing.Size(192, 78);
            this.generatebutton.TabIndex = 9;
            this.generatebutton.Text = "Сгенерировать";
            this.generatebutton.UseVisualStyleBackColor = true;
            this.generatebutton.Click += new System.EventHandler(this.generatebutton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(228)))), ((int)(((byte)(156)))));
            this.label3.Location = new System.Drawing.Point(73, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(307, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Длина парольной фразы:";
            // 
            // passlength
            // 
            this.passlength.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.passlength.Items.Add("30");
            this.passlength.Items.Add("29");
            this.passlength.Items.Add("28");
            this.passlength.Items.Add("27");
            this.passlength.Items.Add("26");
            this.passlength.Items.Add("25");
            this.passlength.Items.Add("24");
            this.passlength.Items.Add("23");
            this.passlength.Items.Add("22");
            this.passlength.Items.Add("21");
            this.passlength.Items.Add("20");
            this.passlength.Items.Add("19");
            this.passlength.Items.Add("18");
            this.passlength.Items.Add("17");
            this.passlength.Items.Add("16");
            this.passlength.Items.Add("15");
            this.passlength.Items.Add("14");
            this.passlength.Items.Add("13");
            this.passlength.Items.Add("12");
            this.passlength.Items.Add("11");
            this.passlength.Items.Add("10");
            this.passlength.Items.Add("9");
            this.passlength.Items.Add("8");
            this.passlength.Items.Add("7");
            this.passlength.Items.Add("6");
            this.passlength.Items.Add("5");
            this.passlength.Items.Add("4");
            this.passlength.Location = new System.Drawing.Point(398, 325);
            this.passlength.Name = "passlength";
            this.passlength.ReadOnly = true;
            this.passlength.Size = new System.Drawing.Size(91, 32);
            this.passlength.TabIndex = 4;
            this.passlength.Text = "8";
            this.passlength.SelectedItemChanged += new System.EventHandler(this.passlength_SelectedItemChanged);
            // 
            // checkBoxSym
            // 
            this.checkBoxSym.AutoSize = true;
            this.checkBoxSym.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxSym.Location = new System.Drawing.Point(73, 269);
            this.checkBoxSym.Name = "checkBoxSym";
            this.checkBoxSym.Size = new System.Drawing.Size(452, 29);
            this.checkBoxSym.TabIndex = 3;
            this.checkBoxSym.Text = "Использовать символы (!?*;$#^&)+-";
            this.checkBoxSym.UseVisualStyleBackColor = true;
            this.checkBoxSym.CheckedChanged += new System.EventHandler(this.checkBoxSym_CheckedChanged);
            // 
            // checkBoxDigit
            // 
            this.checkBoxDigit.AutoSize = true;
            this.checkBoxDigit.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxDigit.Location = new System.Drawing.Point(73, 217);
            this.checkBoxDigit.Name = "checkBoxDigit";
            this.checkBoxDigit.Size = new System.Drawing.Size(283, 29);
            this.checkBoxDigit.TabIndex = 2;
            this.checkBoxDigit.Text = "Использовать цифры";
            this.checkBoxDigit.UseVisualStyleBackColor = true;
            this.checkBoxDigit.CheckedChanged += new System.EventHandler(this.checkBoxDigit_CheckedChanged);
            // 
            // checkBoxRegistr
            // 
            this.checkBoxRegistr.AutoSize = true;
            this.checkBoxRegistr.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxRegistr.Location = new System.Drawing.Point(73, 166);
            this.checkBoxRegistr.Name = "checkBoxRegistr";
            this.checkBoxRegistr.Size = new System.Drawing.Size(512, 29);
            this.checkBoxRegistr.TabIndex = 1;
            this.checkBoxRegistr.Text = "Использовать верхний и нижний регистр";
            this.checkBoxRegistr.UseVisualStyleBackColor = true;
            this.checkBoxRegistr.CheckedChanged += new System.EventHandler(this.checkBoxRegistr_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(228)))), ((int)(((byte)(156)))));
            this.label4.Location = new System.Drawing.Point(73, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(640, 34);
            this.label4.TabIndex = 0;
            this.label4.Text = "Задайте параметры парольной фразы";
            // 
            // GenerateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(30F, 52F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(749, 590);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Verdana", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(11, 8, 11, 8);
            this.Name = "GenerateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Генератор паролей";
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button generatebutton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DomainUpDown passlength;
        private System.Windows.Forms.CheckBox checkBoxSym;
        private System.Windows.Forms.CheckBox checkBoxDigit;
        private System.Windows.Forms.CheckBox checkBoxRegistr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label mes2;
        private System.Windows.Forms.Label mes1;
        private System.Windows.Forms.Button nextbutton;
        private System.Windows.Forms.CheckBox checkBoxshow;
        private System.Windows.Forms.TextBox passtextbox;
    }
}