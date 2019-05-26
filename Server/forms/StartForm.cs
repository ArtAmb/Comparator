using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

namespace Server
{
    public partial class StartForm : Form
    {

        private ComparatorServer comparatorServer = ComparatorServer.getInstance();

        public StartForm()
        {
            InitializeComponent();
            minSentenceWordsNumberInput.Value = 2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string directoryWithFiles = textBox1.Text;
            if (directoryWithFiles == null || directoryWithFiles.Trim().Equals(""))
            {
                this.label2.Text = "Prosze wybraæ folder z plikami do porównania";
                return;
            }

            if (!Directory.Exists(directoryWithFiles))
            {
                this.label2.Text = "Taki folder nie istnieje";
                return;
            }

            if(Decimal.ToInt32(minSentenceWordsNumberInput.Value) < 2 )
            {
                this.label2.Text = "Minimalna liczba s³ów w zdaniu to 2";
                return;
            }

            ComparatorServerInitDTO initDTO = new ComparatorServerInitDTO();
            initDTO.DirectoryWithFiles = directoryWithFiles;
            initDTO.MinSentenceWordsNumber = Decimal.ToInt32(minSentenceWordsNumberInput.Value);

            comparatorServer.start(initDTO);
            new MonitorForm(this).Show();
            Hide();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {

        }

        private void helloWorldLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if(fbd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fbd.SelectedPath;
            }
        }
    }
}
