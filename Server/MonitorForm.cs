using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Server
{
    public partial class MonitorForm : Form
    {
        private Form mainForm = null;
        private ComparatorServer comparatorServer = ComparatorServer.getInstance();

        public MonitorForm(Form mainForm)
        {
            this.mainForm = mainForm;
            InitializeComponent();
        }

        private void MonitorForm_Load(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = comparatorServer.AllFiles;
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
    }
}
