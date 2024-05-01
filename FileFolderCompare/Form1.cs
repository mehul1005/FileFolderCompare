using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace FileFolderCompare
{
    public partial class Form1 : Form
    {
        // Store original folder1 files name in dictionary
        private Dictionary<string, string> folder1FilesDictionary;

        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void buttonCompare_Click(object sender, EventArgs e)
        {
            string folder1 = textBoxFolder1.Text;
            string folder2 = textBoxFolder2.Text;

            if (Directory.Exists(folder1) && Directory.Exists(folder2))
            {
                Dictionary<string, string> folder1Files = GetAllFiles(folder1);
                Dictionary<string, string> folder2Files = GetAllFiles(folder2);

                // Missing files count list
                List<string> missingFiles = GetMissingFiles(folder1Files, folder2Files);

                listBoxInformation.Items.Clear();
                if (missingFiles.Count > 0)
                {
                    foreach (string file in missingFiles)
                    {
                        listBoxInformation.Items.Add(file);
                    }
                }
                else
                {
                    listBoxInformation.Items.Add("No missing files found.");
                }

                // Get missing files count
                lblMissingCount.Text = $"Missing Files: {missingFiles.Count}";
            }
            else
            {
                MessageBox.Show("Please provide valid folder paths.");
            }
        }

        // Store folder2 to compare files name in dictionary
        private Dictionary<string, string> GetAllFiles(string folderPath)
        {
            Dictionary<string, string> files = new Dictionary<string, string>();

            foreach (string file in Directory.GetFiles(folderPath))
            {
                string fileName = Path.GetFileName(file);
                string relativePath = file.Replace(textBoxFolder1.Text, "");
                files[fileName] = relativePath;
            }

            foreach (string subfolder in Directory.GetDirectories(folderPath))
            {
                foreach (KeyValuePair<string, string> file in GetAllFiles(subfolder))
                {
                    files[file.Key] = file.Value;
                }
            }

            return files;
        }

        // Get missing files name 
        private List<string> GetMissingFiles(Dictionary<string, string> folder1Files, Dictionary<string, string> folder2Files)
        {
            List<string> missingFiles = new List<string>();

            foreach (KeyValuePair<string, string> file in folder1Files)
            {
                if (!folder2Files.ContainsKey(file.Key))
                {
                    missingFiles.Add(file.Value);
                }
            }

            return missingFiles;
        }

        private void buttonBrowse1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textBoxFolder1.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void buttonBrowse2_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textBoxFolder2.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }
    }
}
