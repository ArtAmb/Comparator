using Server.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            dataDTO.UniquePairsOfFilesToCompare.CollectionChanged += UniquePairsOfFilesToCompare_CollectionChanged;
            dataDTO.LogMessages.CollectionChanged += LogMessages_CollectionChanged;
        }

        private void UniquePairsOfFilesToCompare_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.pairOfUniqueFilesView.DataSource = null;
            this.pairOfUniqueFilesView.DataSource = DataContainer.read().UniquePairsOfFilesToCompare.Select(el => new PairView(el)).ToList();
            this.pairOfUniqueFilesView.Refresh();
        }

        private void AllClients_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (InvokeRequired)
                Invoke(new Action(() =>
                {
                    AllClients_CollectionChanged(sender, e);
                }));
            else
            {
                this.workersView.DataSource = null;
                this.workersView.DataSource = DataContainer.read().AllClients.Select( el => new WorkerView(el)).ToList();
                this.workersView.Refresh();
            }

        }

        private void LogMessages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            textBox1.Text = String.Join(Environment.NewLine, DataContainer.read().LogMessages);
        }

        private void MonitorForm_Load(object sender, EventArgs e)
        {
            this.allFilesView.DataSource = DataContainer.read().AllFiles;
            this.pairOfUniqueFilesView.DataSource = DataContainer.read().UniquePairsOfFilesToCompare.Select(el => new PairView(el)).ToList();
            this.workersView.DataSource = DataContainer.read().AllClients.Select(el => new WorkerView(el)).ToList();
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

        private void MonitorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            comparatorServer.finish();
        }
    }
}
