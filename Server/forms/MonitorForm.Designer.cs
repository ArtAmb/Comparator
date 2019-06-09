using Server.view;

namespace Server
{
    partial class MonitorForm
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
            this.components = new System.ComponentModel.Container();
            this.helloWorldLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.allFilesView = new System.Windows.Forms.DataGridView();
            this.pairOfUniqueFilesView = new System.Windows.Forms.DataGridView();
            this.PairUUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ComparingResult = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.workersView = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.generateSummaryBtn = new System.Windows.Forms.Button();
            this.processorInfoBtn = new System.Windows.Forms.Button();
            this.file1NameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.file2NameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.resultFileDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pairViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.allFilesView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pairOfUniqueFilesView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.workersView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pairViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // helloWorldLabel
            // 
            this.helloWorldLabel.AutoSize = true;
            this.helloWorldLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helloWorldLabel.Location = new System.Drawing.Point(414, 9);
            this.helloWorldLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.helloWorldLabel.Name = "helloWorldLabel";
            this.helloWorldLabel.Size = new System.Drawing.Size(322, 26);
            this.helloWorldLabel.TabIndex = 4;
            this.helloWorldLabel.Text = "Rozproszony komparator plików";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 369);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // allFilesView
            // 
            this.allFilesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.allFilesView.Location = new System.Drawing.Point(12, 77);
            this.allFilesView.Name = "allFilesView";
            this.allFilesView.ReadOnly = true;
            this.allFilesView.Size = new System.Drawing.Size(457, 257);
            this.allFilesView.TabIndex = 6;
            this.allFilesView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.allFilesView_CellContentClick);
            // 
            // pairOfUniqueFilesView
            // 
            this.pairOfUniqueFilesView.AllowUserToAddRows = false;
            this.pairOfUniqueFilesView.AllowUserToDeleteRows = false;
            this.pairOfUniqueFilesView.AutoGenerateColumns = false;
            this.pairOfUniqueFilesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.pairOfUniqueFilesView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.file1NameDataGridViewTextBoxColumn,
            this.PairUUID,
            this.file2NameDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn,
            this.resultFileDataGridViewTextBoxColumn,
            this.ComparingResult});
            this.pairOfUniqueFilesView.DataSource = this.pairViewBindingSource;
            this.pairOfUniqueFilesView.Location = new System.Drawing.Point(675, 77);
            this.pairOfUniqueFilesView.Name = "pairOfUniqueFilesView";
            this.pairOfUniqueFilesView.ReadOnly = true;
            this.pairOfUniqueFilesView.Size = new System.Drawing.Size(467, 257);
            this.pairOfUniqueFilesView.TabIndex = 7;
            this.pairOfUniqueFilesView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.pairOfUniqueFilesView_CellContentClick);
            // 
            // PairUUID
            // 
            this.PairUUID.DataPropertyName = "PairUUID";
            this.PairUUID.HeaderText = "PairUUID";
            this.PairUUID.Name = "PairUUID";
            this.PairUUID.ReadOnly = true;
            this.PairUUID.Visible = false;
            // 
            // ComparingResult
            // 
            this.ComparingResult.HeaderText = "ComparingResult";
            this.ComparingResult.Name = "ComparingResult";
            this.ComparingResult.ReadOnly = true;
            this.ComparingResult.Text = "Podgląd";
            this.ComparingResult.UseColumnTextForButtonValue = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 26);
            this.label1.TabIndex = 8;
            this.label1.Text = "Wszystkie pliki";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(670, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 26);
            this.label2.TabIndex = 9;
            this.label2.Text = "Unikalne pary plików";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.textBox1.Location = new System.Drawing.Point(675, 543);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(467, 149);
            this.textBox1.TabIndex = 10;
            // 
            // workersView
            // 
            this.workersView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.workersView.Location = new System.Drawing.Point(16, 435);
            this.workersView.Name = "workersView";
            this.workersView.ReadOnly = true;
            this.workersView.Size = new System.Drawing.Size(457, 257);
            this.workersView.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 406);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 26);
            this.label3.TabIndex = 12;
            this.label3.Text = "Workers";
            // 
            // generateSummaryBtn
            // 
            this.generateSummaryBtn.Location = new System.Drawing.Point(1003, 340);
            this.generateSummaryBtn.Name = "generateSummaryBtn";
            this.generateSummaryBtn.Size = new System.Drawing.Size(139, 23);
            this.generateSummaryBtn.TabIndex = 13;
            this.generateSummaryBtn.Text = "Generuj podsumowanie";
            this.generateSummaryBtn.UseVisualStyleBackColor = true;
            this.generateSummaryBtn.Click += new System.EventHandler(this.generateSummaryBtn_Click);
            // 
            // processorInfoBtn
            // 
            this.processorInfoBtn.Location = new System.Drawing.Point(12, 340);
            this.processorInfoBtn.Name = "processorInfoBtn";
            this.processorInfoBtn.Size = new System.Drawing.Size(122, 23);
            this.processorInfoBtn.TabIndex = 14;
            this.processorInfoBtn.Text = "Get processor info";
            this.processorInfoBtn.UseVisualStyleBackColor = true;
            this.processorInfoBtn.Click += new System.EventHandler(this.processorInfoBtn_Click);
            // 
            // file1NameDataGridViewTextBoxColumn
            // 
            this.file1NameDataGridViewTextBoxColumn.DataPropertyName = "File1Name";
            this.file1NameDataGridViewTextBoxColumn.HeaderText = "File1Name";
            this.file1NameDataGridViewTextBoxColumn.Name = "file1NameDataGridViewTextBoxColumn";
            this.file1NameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // file2NameDataGridViewTextBoxColumn
            // 
            this.file2NameDataGridViewTextBoxColumn.DataPropertyName = "File2Name";
            this.file2NameDataGridViewTextBoxColumn.HeaderText = "File2Name";
            this.file2NameDataGridViewTextBoxColumn.Name = "file2NameDataGridViewTextBoxColumn";
            this.file2NameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn.HeaderText = "State";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            this.stateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // resultFileDataGridViewTextBoxColumn
            // 
            this.resultFileDataGridViewTextBoxColumn.DataPropertyName = "ResultFile";
            this.resultFileDataGridViewTextBoxColumn.HeaderText = "ResultFile";
            this.resultFileDataGridViewTextBoxColumn.Name = "resultFileDataGridViewTextBoxColumn";
            this.resultFileDataGridViewTextBoxColumn.ReadOnly = true;
            this.resultFileDataGridViewTextBoxColumn.Visible = false;
            // 
            // pairViewBindingSource
            // 
            this.pairViewBindingSource.DataSource = typeof(Server.view.PairView);
            // 
            // MonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 704);
            this.Controls.Add(this.processorInfoBtn);
            this.Controls.Add(this.generateSummaryBtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.workersView);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pairOfUniqueFilesView);
            this.Controls.Add(this.allFilesView);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.helloWorldLabel);
            this.Name = "MonitorForm";
            this.Text = "Rozproszony komparator plików";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MonitorForm_FormClosed);
            this.Load += new System.EventHandler(this.MonitorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.allFilesView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pairOfUniqueFilesView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.workersView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pairViewBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label helloWorldLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView allFilesView;
        private System.Windows.Forms.DataGridView pairOfUniqueFilesView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridView workersView;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button generateSummaryBtn;
        private System.Windows.Forms.Button processorInfoBtn;
        private System.Windows.Forms.BindingSource pairViewBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn file1NameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PairUUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn file2NameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn resultFileDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewButtonColumn ComparingResult;
    }
}