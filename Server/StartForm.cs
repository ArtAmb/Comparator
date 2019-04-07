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


            comparatorServer.start(directoryWithFiles);
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
