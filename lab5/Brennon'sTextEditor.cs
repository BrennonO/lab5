// Brennon Ouellette
// NETD2202
// April 4, 2022

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace lab5
{
    public partial class formBrennonsTextEditor : Form
    {
        //This variable represents the current file that has been opened for editing.
        string openFile = String.Empty;

        public formBrennonsTextEditor()
        {
            InitializeComponent();
        }

        #region "Event Handlers"

        /// <summary>
        /// Me close form.
        /// </summary>
        private void ExitClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Open a save dialog and allow the user to pick a location, then save the current text content to the file.
        /// </summary>
        private void SaveAsClick(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Text Files (*.txt) | *.txt";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                openFile = saveDialog.FileName;

                SaveFile(openFile);
            }

            UpdateTitle();
        }
        /// <summary>
        /// If we know which file is open, save it. If we don't, run "Save As".
        /// </summary>
        private void SaveClick(object sender, EventArgs e)
        {
            // if openFile (the file tha is currently open has no value, call "save As" instead!
            if (openFile == String.Empty)
            {
                SaveAsClick(sender, e);
            }

            // If openFile has a value, just call the SaveFile() function!
            else
            {
                SaveFile(openFile);
            }
        }

        private void NewClick(object sender, EventArgs e)
        {
            textBoxBody.Clear();
            openFile = String.Empty;

            UpdateTitle();
        }

        private void OpenClick(object sender, EventArgs e)
        {

            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Text Files (*.txt) | *.txt";

            // if user has picked a file
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream fileToAccess = new FileStream(openDialog.FileName, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(fileToAccess);

                textBoxBody.Clear();

                while (!reader.EndOfStream)
                {
                    string newTextBoxBody = reader.ReadLine();
                    textBoxBody.Text += newTextBoxBody;
                }

                reader.Close();
            }

            UpdateTitle();
        }

        private void CopyClick(object sender, EventArgs e)
        {
            if (textBoxBody.SelectionLength > 0)
            {
                Clipboard.SetText(textBoxBody.SelectedText);
            }
        }

        private void CutClick(object sender, EventArgs e)
        {
            textBoxBody.Cut();
        }

        private void PasteClick(object sender, EventArgs e)
        {
            Clipboard.GetText();
            textBoxBody.Paste();
        }

        /// <summary>
        /// Displays a message box about the application.
        /// </summary>
        private void AboutClick(object sender, EventArgs e)
        {
            MessageBox.Show("Brennon's Text Editor\n\nAn Notepad ripoff, by Brennon Ouellette.\nFor NETD 2202, April 2022");
        }
        #endregion

        #region "Functions"

        /// <summary>
        /// This will sae the file at the path indicated by the filename passed in.
        /// </summary>
        /// <param name="fileName">It's a filename!</param>
        private void SaveFile(string fileName)
        {
            FileStream fileToAccess = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(fileToAccess);

            writer.Write(textBoxBody.Text);

            writer.Close();
        }

        /// <summary>
        /// Updates the Text property of the window to reflect any open file.
        /// </summary>
        private void UpdateTitle()
        {
            this.Text = "Brennon's Text Editor";

            if (openFile != String.Empty)
            {
                this.Text += " - " + openFile;
            }
        }

        #endregion
    }
}
