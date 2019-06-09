using Server.forms;
using Server.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WcfServiceLibrary1;

namespace Server
{
    public partial class MonitorForm : Form
    {
        private Form mainForm = null;
        private ComparingResultViewForm comparingResultViewForm = null;
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
                this.workersView.DataSource = DataContainer.read().AllClients.Select(el => new WorkerView(el)).ToList();
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
            if(comparingResultViewForm != null)
                comparingResultViewForm.Close();
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

        private void generateSummaryBtn_Click(object sender, EventArgs e)
        {
            String comparedFilesHeader = String.Join(@";", "File1", "File2", "Ip", "Port", "ProcessorInfo", "ComparingTime", "DownloadFile1Time", "DownloadFile2Time", "SendingResultTime", "TotalNewtworkTransferTime");
            String notComparedFilesHeader = @"File1;File2";

            List<String> uncomparedFilesRows = DataContainer.read().UniquePairsOfFilesToCompare
               .Where(pairOfFiles => pairOfFiles.ComparingResult == null)
               .Select(pair =>
               {
                   return String.Format("{0};{1}", pair.FileName1, pair.FileName2);
               }).ToList();

            String notComparedFilesSummary = uncomparedFilesRows.Count() == 0 ? "" : String.Join(Environment.NewLine, uncomparedFilesRows);

            List<String> rows = DataContainer.read().UniquePairsOfFilesToCompare
                .Where(pairOfFiles => pairOfFiles.ComparingResult != null)
                .Select(pair =>
                                {
                                    var client = getClient(pair.ComparingResult.ClientUUID);
                                    var result = pair.ComparingResult;

                                    return String.Join(@";",
                                        pair.FileName1,
                                        pair.FileName2,
                                        client.Ip,
                                        client.Port,
                                        client.ProcessorInfo.Replace(Environment.NewLine, ""),
                                        result.ComparingTime.Milliseconds,
                                        result.File1DownloadingTime.Milliseconds,
                                        result.File2DownloadingTime.Milliseconds,
                                        result.SendingTime.Milliseconds,
                                        result.File1DownloadingTime.Milliseconds + result.File2DownloadingTime.Milliseconds + result.SendingTime.Milliseconds
                                        );
                                }).ToList();


            String comparedFilesSummary = String.Join(Environment.NewLine, rows);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(comparedFilesHeader);
            sb.Append(comparedFilesSummary);
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine();
            if (uncomparedFilesRows.Count() != 0)
            {
                sb.AppendLine("NotComparedFiles");
                sb.AppendLine(notComparedFilesHeader);
                sb.Append(notComparedFilesSummary);
            }

            String summaryContent = comparedFilesSummary;


            var bytes = Encoding.ASCII.GetBytes(sb.ToString());
            String filePathRoot = DataContainer.read().PathToFiles + @"\comparing_summary";
            if (!Directory.Exists(filePathRoot))
            {
                Directory.CreateDirectory(filePathRoot);
            }
            String strDate = DateTime.Now.ToString();
            strDate = strDate.Replace(":", "-").Replace(" ", "_");

            String newFileName = @"comparing_summary_" + strDate + ".csv";
            using (var fileStream = File.OpenWrite(filePathRoot + @"\" + newFileName))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }

            DataContainer.read().LogMessages.Add("Podsumowanie zostało zapisane w pliku: " + newFileName);
        }

        private Worker getClient(String clientUUID)
        {
            IEnumerable<Worker> clients = DataContainer.read().AllClients.Where(cl => cl.Uuid.Equals(clientUUID));
            if (clients.Count() == 0)
            {
                Worker tmp = new Worker();
                tmp.Ip = "-1";
                tmp.Port = "-1";
                tmp.ProcessorInfo = "";
                return tmp;
            }

            return clients.First();
        }

        private void processorInfoBtn_Click(object sender, EventArgs e)
        {
            DataContainer.read().LogMessages.Add(new ProcessorService().getProcessorInfo());
        }

        private void showComparingResultBtn_Click(object sender, EventArgs e)
        {
          
        }

        private void closeComparingFrom(object sender, EventArgs e)
        {
            comparingResultViewForm = null;
        }

        private void pairOfUniqueFilesView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(pairOfUniqueFilesView.Columns[e.ColumnIndex].Name == "ComparingResult" && e.RowIndex >= 0)
            {
                
                if (comparingResultViewForm != null)
                    comparingResultViewForm.Close();
                
                String pairUUID = (String)pairOfUniqueFilesView.Rows[e.RowIndex].Cells["pairUUID"].Value;

                FilesToCompare selectedPair = DataContainer.read()
                    .UniquePairsOfFilesToCompare
                    .Where(pair => pair.Id.Equals(pairUUID))
                    .First();

                if(selectedPair.ComparingResult == null)
                {
                    MessageBox.Show(String.Format("Pliki: {0} oraz {1} nie zostały jeszcze porównane", selectedPair.FileName1, selectedPair.FileName2));
                    return;
                }

                comparingResultViewForm = new ComparingResultViewForm(selectedPair);
                comparingResultViewForm.FormClosed += closeComparingFrom;
                comparingResultViewForm.Show();
            }
        }

        private void allFilesView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
