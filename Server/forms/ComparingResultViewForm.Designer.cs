namespace Server.forms
{
    partial class ComparingResultViewForm
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
            this.file2ContentBox = new System.Windows.Forms.RichTextBox();
            this.file1Name = new System.Windows.Forms.Label();
            this.file2Name = new System.Windows.Forms.Label();
            this.commonSentencesGridView = new System.Windows.Forms.DataGridView();
            this.F1StartIdx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F1EndIdx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F2StartIdx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.F2EndIdx = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sentence = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.file1ContentBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.errorLabel = new System.Windows.Forms.Label();
            this.logMsgLabel = new System.Windows.Forms.Label();
            this.nrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comonSentenceViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.checkAllCommonsBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.commonSentencesGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comonSentenceViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // file2ContentBox
            // 
            this.file2ContentBox.Location = new System.Drawing.Point(626, 49);
            this.file2ContentBox.Name = "file2ContentBox";
            this.file2ContentBox.ReadOnly = true;
            this.file2ContentBox.Size = new System.Drawing.Size(451, 523);
            this.file2ContentBox.TabIndex = 1;
            this.file2ContentBox.Text = "";
            this.file2ContentBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // file1Name
            // 
            this.file1Name.AutoSize = true;
            this.file1Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.file1Name.Location = new System.Drawing.Point(177, 9);
            this.file1Name.Name = "file1Name";
            this.file1Name.Size = new System.Drawing.Size(104, 31);
            this.file1Name.TabIndex = 3;
            this.file1Name.Text = "Tekst 1";
            // 
            // file2Name
            // 
            this.file2Name.AutoSize = true;
            this.file2Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.file2Name.Location = new System.Drawing.Point(808, 9);
            this.file2Name.Name = "file2Name";
            this.file2Name.Size = new System.Drawing.Size(104, 31);
            this.file2Name.TabIndex = 4;
            this.file2Name.Text = "Tekst 2";
            this.file2Name.Click += new System.EventHandler(this.label2_Click);
            // 
            // commonSentencesGridView
            // 
            this.commonSentencesGridView.AllowUserToAddRows = false;
            this.commonSentencesGridView.AllowUserToDeleteRows = false;
            this.commonSentencesGridView.AutoGenerateColumns = false;
            this.commonSentencesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.commonSentencesGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nrDataGridViewTextBoxColumn,
            this.F1StartIdx,
            this.F1EndIdx,
            this.F2StartIdx,
            this.F2EndIdx,
            this.Sentence});
            this.commonSentencesGridView.DataSource = this.comonSentenceViewBindingSource;
            this.commonSentencesGridView.Location = new System.Drawing.Point(497, 49);
            this.commonSentencesGridView.Name = "commonSentencesGridView";
            this.commonSentencesGridView.ReadOnly = true;
            this.commonSentencesGridView.RowHeadersVisible = false;
            this.commonSentencesGridView.Size = new System.Drawing.Size(99, 494);
            this.commonSentencesGridView.TabIndex = 6;
            this.commonSentencesGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.commonSentencesGridView_CellContentClick);
            // 
            // F1StartIdx
            // 
            this.F1StartIdx.DataPropertyName = "F1StartIdx";
            this.F1StartIdx.HeaderText = "F1StartIdx";
            this.F1StartIdx.Name = "F1StartIdx";
            this.F1StartIdx.ReadOnly = true;
            this.F1StartIdx.Visible = false;
            // 
            // F1EndIdx
            // 
            this.F1EndIdx.DataPropertyName = "F1EndIdx";
            this.F1EndIdx.HeaderText = "F1EndIdx";
            this.F1EndIdx.Name = "F1EndIdx";
            this.F1EndIdx.ReadOnly = true;
            this.F1EndIdx.Visible = false;
            // 
            // F2StartIdx
            // 
            this.F2StartIdx.DataPropertyName = "F2StartIdx";
            this.F2StartIdx.HeaderText = "F2StartIdx";
            this.F2StartIdx.Name = "F2StartIdx";
            this.F2StartIdx.ReadOnly = true;
            this.F2StartIdx.Visible = false;
            // 
            // F2EndIdx
            // 
            this.F2EndIdx.DataPropertyName = "F2EndIdx";
            this.F2EndIdx.HeaderText = "F2EndIdx";
            this.F2EndIdx.Name = "F2EndIdx";
            this.F2EndIdx.ReadOnly = true;
            this.F2EndIdx.Visible = false;
            // 
            // Sentence
            // 
            this.Sentence.DataPropertyName = "Sentence";
            this.Sentence.HeaderText = "Sentence";
            this.Sentence.Name = "Sentence";
            this.Sentence.ReadOnly = true;
            this.Sentence.Visible = false;
            // 
            // file1ContentBox
            // 
            this.file1ContentBox.Location = new System.Drawing.Point(12, 49);
            this.file1ContentBox.Name = "file1ContentBox";
            this.file1ContentBox.ReadOnly = true;
            this.file1ContentBox.Size = new System.Drawing.Size(453, 523);
            this.file1ContentBox.TabIndex = 7;
            this.file1ContentBox.Text = "";
            this.file1ContentBox.TextChanged += new System.EventHandler(this.file1ContentBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(471, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Części wspólne";
            // 
            // errorLabel
            // 
            this.errorLabel.AutoSize = true;
            this.errorLabel.ForeColor = System.Drawing.Color.Red;
            this.errorLabel.Location = new System.Drawing.Point(465, 574);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(0, 13);
            this.errorLabel.TabIndex = 10;
            // 
            // logMsgLabel
            // 
            this.logMsgLabel.AutoSize = true;
            this.logMsgLabel.Location = new System.Drawing.Point(465, 595);
            this.logMsgLabel.Name = "logMsgLabel";
            this.logMsgLabel.Size = new System.Drawing.Size(0, 13);
            this.logMsgLabel.TabIndex = 11;
            // 
            // nrDataGridViewTextBoxColumn
            // 
            this.nrDataGridViewTextBoxColumn.DataPropertyName = "Nr";
            this.nrDataGridViewTextBoxColumn.HeaderText = "Nr";
            this.nrDataGridViewTextBoxColumn.Name = "nrDataGridViewTextBoxColumn";
            this.nrDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // comonSentenceViewBindingSource
            // 
            this.comonSentenceViewBindingSource.DataSource = typeof(Server.view.ComonSentenceView);
            // 
            // checkAllCommonsBtn
            // 
            this.checkAllCommonsBtn.Location = new System.Drawing.Point(497, 549);
            this.checkAllCommonsBtn.Name = "checkAllCommonsBtn";
            this.checkAllCommonsBtn.Size = new System.Drawing.Size(99, 23);
            this.checkAllCommonsBtn.TabIndex = 12;
            this.checkAllCommonsBtn.Text = "Zaznacz wszytkie";
            this.checkAllCommonsBtn.UseVisualStyleBackColor = true;
            this.checkAllCommonsBtn.Click += new System.EventHandler(this.checkAllCommonsBtn_Click);
            // 
            // ComparingResultViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 635);
            this.Controls.Add(this.checkAllCommonsBtn);
            this.Controls.Add(this.logMsgLabel);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.file1ContentBox);
            this.Controls.Add(this.commonSentencesGridView);
            this.Controls.Add(this.file2Name);
            this.Controls.Add(this.file1Name);
            this.Controls.Add(this.file2ContentBox);
            this.Name = "ComparingResultViewForm";
            this.Text = "Porównanie plików";
            ((System.ComponentModel.ISupportInitialize)(this.commonSentencesGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comonSentenceViewBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox file2ContentBox;
        private System.Windows.Forms.Label file1Name;
        private System.Windows.Forms.Label file2Name;
        private System.Windows.Forms.DataGridView commonSentencesGridView;
        private System.Windows.Forms.RichTextBox file1ContentBox;
        private System.Windows.Forms.BindingSource comonSentenceViewBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn nrDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn F1StartIdx;
        private System.Windows.Forms.DataGridViewTextBoxColumn F1EndIdx;
        private System.Windows.Forms.DataGridViewTextBoxColumn F2StartIdx;
        private System.Windows.Forms.DataGridViewTextBoxColumn F2EndIdx;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sentence;
        private System.Windows.Forms.Label logMsgLabel;
        private System.Windows.Forms.Button checkAllCommonsBtn;
    }
}