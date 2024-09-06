using System;
using System.IO;
using System.Windows.Forms;

namespace KURS_ZD
{
    public partial class step1 : Form
    {
        string Content = string.Empty;
        bool exit = true;
        public step1()
        {
            InitializeComponent();
            ActiveControl = DataBox;
        }

        private void LoadDataButton_Click(object sender, EventArgs e)
        {
            if (LoadDataButton.Text == "Загрузить из файла")
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.InitialDirectory = "c:\\";
                    openFileDialog.Filter = "All files (*.*)|*.*";
                    openFileDialog.RestoreDirectory = true;

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        DataBox.Text = "Выбран файл:  " + openFileDialog.FileName;
                        Content = openFileDialog.FileName;
                    }
                    if (!string.IsNullOrEmpty(openFileDialog.FileName))
                        LoadDataButton.Text = "Отменить загрузку файла";
                }        
            }
            else
            {
                Content = string.Empty;
                DataBox.Text = string.Empty;
                LoadDataButton.Text = "Загрузить из файла";
            }
                
        }
        private void backButton_Click(object sender, EventArgs e)
        {
            BeginForm workForm = new BeginForm();
            workForm.Show();
            exit = false;
            this.Close();
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DataBox.Text))
                MessageBox.Show("Пожалуйста введите данные для шифрования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                if (string.IsNullOrEmpty(Content))
                    Content = "TEXT" + DataBox.Text;
                step2 workForm = new step2(Content);
                workForm.Show();
                exit = false;
                this.Close();
            }
        }

        private void step1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (exit)
                Application.Exit();
        }
    }
}
