using Server.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WcfServiceLibrary1;

namespace Server.forms
{
    public partial class ComparingResultViewForm : Form
    {

        public ComparingResultViewForm(FilesToCompare pair)
        {
            InitializeComponent();

            file1Name.Text = pair.FileName1;
            file2Name.Text = pair.FileName2;

            String file1Path = DataContainer.read().PathToFiles + @"\" + pair.FileName1;
            String file2Path = DataContainer.read().PathToFiles + @"\" + pair.FileName2;

            String file1Content = loadFile(file1Path);
            String file2Content = loadFile(file2Path);

            int sentenceNumber = 1;

            List<ComonSentenceView> commonSentencesView = pair.ComparingResult.CommonSentences.Select(comSen =>
            {
                return new ComonSentenceView()
                {
                    Nr = sentenceNumber++,
                    Sentence = getSentence(file1Content.Split(null), comSen.File1),
                    F1StartIdx = comSen.File1.FirstWordIndex,
                    F1EndIdx = comSen.File1.LastWordIndexIndex,
                    F2StartIdx = comSen.File2.FirstWordIndex,
                    F2EndIdx = comSen.File2.LastWordIndexIndex
                };
            }).ToList();

            commonSentencesGridView.DataSource = commonSentencesView;


            initFileContentBox(file1ContentBox, file1Content);
            initFileContentBox(file2ContentBox, file2Content);


            //file1ContentBox.Find()

        }

        private String getSentence(String[] fileSetences, SentenceIndexes indexes)
        {
            String[] wordsOfSentence = new String[indexes.getLength()];
            Array.Copy(fileSetences, indexes.FirstWordIndex, wordsOfSentence, 0, indexes.getLength());

            return String.Join(" ", wordsOfSentence);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private String loadFile(String pathToFile)
        {
            MemoryStream stream = new MemoryStream();
            var bytes = File.ReadAllBytes(pathToFile);
            stream.Write(bytes, 0, bytes.Length);
            stream.Position = 0;

            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd().Trim() + " ";//.Split(null);

        }

        private void initFileContentBox(RichTextBox fileContentBox, String content)
        {
            fileContentBox.BackColor = Color.White;
            fileContentBox.Text = content;
        }

        private void commonSentencesGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                clearSelection(file1ContentBox);
                clearSelection(file2ContentBox);
                
                highlightCommonSentence(e.RowIndex);
            }
        }

        private void clearSelection(RichTextBox fileContectBox)
        {
            fileContectBox.SelectAll();
            fileContectBox.SelectionBackColor = Color.White;
        }

        private void highlightCommonSentence(int rowIndex)
        {
            var row = commonSentencesGridView.Rows[rowIndex];
            String commonSentence = (String)row.Cells["Sentence"].Value;
            int f1StartIdx = (int)row.Cells["F1StartIdx"].Value;
            int f1EndIdx = (int)row.Cells["F1EndIdx"].Value;

            int f2StartIdx = (int)row.Cells["F2StartIdx"].Value;
            int f2EndIdx = (int)row.Cells["F2EndIdx"].Value;

            logMsgLabel.Text = String.Format("f1 {0} -> {1}, f2 {2} -> {3}", f1StartIdx, f1EndIdx, f2StartIdx, f2EndIdx);

            highlightSentence(file1Name.Text, rowIndex + 1, file1ContentBox, commonSentence, new SentenceIndexes() { FirstWordIndex = f1StartIdx, LastWordIndexIndex = f1EndIdx });
            highlightSentence(file2Name.Text, rowIndex + 1, file2ContentBox, commonSentence, new SentenceIndexes() { FirstWordIndex = f2StartIdx, LastWordIndexIndex = f2EndIdx });
        }

        private void highlightSentence(String fileName, int sentenceNum, RichTextBox fileContectBox, String sentence, SentenceIndexes sentenceIndexes)
        {
            String content = fileContectBox.Text;

            int wordCounter = -1;
            int firstCharOfStartWordIdx = -1;
            int lastCharOfEndWordIdx = -1;
            bool isCurrentIndexOnWord = true;
            bool isPreviousIndexOnWord = true;
            int firstCharOfWordIdx = 0;

            for (int index = 0; index < content.Length; ++index)
            {
                isPreviousIndexOnWord = isCurrentIndexOnWord;

                if (Char.IsWhiteSpace(content, index))
                {
                    isCurrentIndexOnWord = false;
                }
                else
                {
                    isCurrentIndexOnWord = true;
                }
                if (isStartOfNewWord(isPreviousIndexOnWord, isCurrentIndexOnWord))
                {
                    firstCharOfWordIdx = index;
                }

                if (isEndOfWord(isPreviousIndexOnWord, isCurrentIndexOnWord))
                {
                    ++wordCounter;
                    if (wordCounter == sentenceIndexes.FirstWordIndex)
                    {
                        firstCharOfStartWordIdx = firstCharOfWordIdx;
                    }

                    if (wordCounter == sentenceIndexes.LastWordIndexIndex)
                    {
                        lastCharOfEndWordIdx = index - 1;
                        break;
                    }
                }
            }

            errorLabel.Text = String.Format("File = {0}, NR = {1}, firstIdx = {2} lastIdx = {3}",
                    fileName,
                    sentenceNum,
                    firstCharOfStartWordIdx,
                    lastCharOfEndWordIdx);

            if (firstCharOfStartWordIdx < 0 || lastCharOfEndWordIdx < 0)
            {
                string errMsg = "ERROR -> " + String.Format("File = {0}, NR = {1}, firstIdx = {2} lastIdx = {3}",
                    fileName,
                    sentenceNum,
                    firstCharOfStartWordIdx,
                    lastCharOfEndWordIdx);

                errorLabel.Text = errMsg;
                return;
            }

            //fileContectBox.Find(sentence, firstCharOfStartWordIdx, lastCharOfEndWordIdx, RichTextBoxFinds.WholeWord);
            fileContectBox.Select(firstCharOfStartWordIdx, lastCharOfEndWordIdx - firstCharOfStartWordIdx + 1);
            fileContectBox.SelectionBackColor = Color.Yellow;
        }

        private bool isStartOfNewWord(bool isPreviousIndexOnWord, bool isCurrentIndexOnWord)
        {
            return !isPreviousIndexOnWord && isCurrentIndexOnWord;
        }

        private bool isEndOfWord(bool isPreviousIndexOnWord, bool isCurrentIndexOnWord)
        {
            return isPreviousIndexOnWord && !isCurrentIndexOnWord;
        }

        private void file1ContentBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkAllCommonsBtn_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < commonSentencesGridView.RowCount; ++i)
            {
                highlightCommonSentence(i);
            }
            
        }
    }
}
