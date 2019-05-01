using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using WcfServiceLibrary1;

namespace Server
{
    public partial class MonitorForm : Form
    {
        private Form mainForm = null;
        private ComparatorServer comparatorServer = ComparatorServer.getInstance();

        public MonitorForm(Form mainForm)
        {
            DataDTO dataDTO = DataContainer.read();

            this.mainForm = mainForm;
            InitializeComponent();

            dataDTO.AllClients.CollectionChanged += AllClients_CollectionChanged;
            dataDTO.LogMessages.CollectionChanged += LogMessages_CollectionChanged;
        }

        private void AllClients_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.workersView.DataSource = null;
            this.workersView.DataSource = DataContainer.read().AllClients;
        }

        private void LogMessages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            textBox1.Text = String.Join(Environment.NewLine, DataContainer.read().LogMessages);
        }

        private void MonitorForm_Load(object sender, EventArgs e)
        {
            this.allFilesView.DataSource = DataContainer.read().AllFiles;
            this.pairOfUniqueFilesView.DataSource = DataContainer.read().UniquePairsOfFilesToCompare;
            this.workersView.DataSource = DataContainer.read().AllClients;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            mainForm.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataContainer.read().LogMessages.Add("Dzialam");
            var w = new Worker();
            w.Ip = "Dzialam";

            DataContainer.read().AllClients.Add(w);
        }
    }
}
