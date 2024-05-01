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
using MetroFramework.Components;
using MetroFramework;
using MetroFramework.Forms;
using MetroFramework.Controls;

namespace FileFolderCompare
{
    public partial class Form2 : MetroForm
    {
        // Store original folder1 files name in dictionary
        private Dictionary<string, string> folder1FilesDictionary;
        private bool isTransferButtonDisabled = false;

        public Form2()
        {
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void buttonCompareM_Click(object sender, EventArgs e)
        {
            string folder1 = textBoxFolder1M.Text;
            string folder2 = textBoxFolder2M.Text;

            if (Directory.Exists(folder1) && Directory.Exists(folder2))
            {
                Dictionary<string, string> folder1Files = GetAllFiles(folder1);
                Dictionary<string, string> folder2Files = GetAllFiles(folder2);

                // Missing files count list
                List<string> missingFiles = GetMissingFiles(folder1Files, folder2Files);

                listBoxInformationM.Items.Clear();
                if (missingFiles.Count > 0)
                {
                    foreach (string file in missingFiles)
                    {
                        listBoxInformationM.Items.Add(file);
                    }
                }
                else
                {
                    listBoxInformationM.Items.Add("No missing files found.");
                }

                // Get missing files count
                lblMissingCountM.Text = $"Missing Files: {missingFiles.Count}";
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
                string fullPath = Path.GetFullPath(file); // Get the full path of the file
                files[fileName] = fullPath;
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

        private void buttonBrowse1M_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textBoxFolder1M.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void buttonBrowse2M_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textBoxFolder2M.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void buttonTransferM_Click(object sender, EventArgs e)
        {
            string destinationFolder = textBoxDestinationFolderM.Text;

            if (!Directory.Exists(destinationFolder))
            {
                MessageBox.Show("Please provide a valid destination folder path.");
                return;
            }

            List<string> missingFiles = GetMissingFiles(GetAllFiles(textBoxFolder1M.Text), GetAllFiles(textBoxFolder2M.Text));

            int transferredFileCount = 0;
            int totalFilesCount = missingFiles.Count;

            metroProgressBar1.Maximum = totalFilesCount;
            metroProgressBar1.Value = 0;

            foreach (string missingFile in missingFiles)
            {
                if (missingFile.EndsWith(".cap") || missingFile.EndsWith(".adv"))
                {
                    string sourceFilePath = Path.Combine(textBoxFolder1M.Text, missingFile);
                    string destinationFilePath = Path.Combine(destinationFolder, Path.GetFileName(missingFile));

                    try
                    {
                        File.Copy(sourceFilePath, destinationFilePath);
                        transferredFileCount++;

                        // Update progress bar
                        metroProgressBar1.Value = transferredFileCount;
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that occurred during file copying
                        MessageBox.Show($"Error occurred while copying file: {missingFile}\n\n{ex.Message}");
                    }
                }
            }

            MessageBox.Show($"{transferredFileCount} files copied successfully.");
            metroProgressBar1.Value = 0; // Reset progress bar value
        }

        private void buttonBrowse3M_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    textBoxDestinationFolderM.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        // Get missing files name with full path
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

        // Get missing files with different extensions
        private List<string> GetMissingDifferentExtensions(Dictionary<string, string> folder1Files, Dictionary<string, string> folder2Files)
        {
            List<string> missingFiles = new List<string>();

            foreach (KeyValuePair<string, string> file in folder1Files)
            {
                string fileExtension = Path.GetExtension(file.Key);
                if (!string.IsNullOrEmpty(fileExtension) && (fileExtension == ".cap" || fileExtension == ".adv" || fileExtension == ".csv" || fileExtension == ".txt"))
                {
                    string txtFileName = Path.GetFileNameWithoutExtension(file.Key) + ".txt";
                    string csvFileName = Path.GetFileNameWithoutExtension(file.Key) + ".csv"; // New line

                    if (!folder2Files.ContainsKey(txtFileName) && !folder2Files.ContainsKey(csvFileName)) // Updated condition
                    {
                        missingFiles.Add(file.Value);
                    }
                }
            }

            return missingFiles;
        }

        private void buttonTxtCompareM_Click(object sender, EventArgs e)
        {
            string folder1 = textBoxFolder1M.Text;
            string folder2 = textBoxFolder2M.Text;
            string destinationFolder = textBoxDestinationFolderM.Text;

            if (Directory.Exists(folder1) && Directory.Exists(folder2) && Directory.Exists(destinationFolder))
            {
                Dictionary<string, string> folder1Files = GetAllFiles(folder1);
                Dictionary<string, string> folder2Files = GetAllFiles(folder2);

                // Missing files list with different extensions
                List<string> missingFiles = GetMissingDifferentExtensions(folder1Files, folder2Files);

                listBoxInformationM.Items.Clear();
                if (missingFiles.Count > 0)
                {
                    foreach (string file in missingFiles)
                    {
                        listBoxInformationM.Items.Add(file);
                        string sourceFilePath = Path.Combine(folder1, file);
                        string destinationFilePath = Path.Combine(destinationFolder, Path.GetFileName(file));

                        try
                        {
                            File.Copy(sourceFilePath, destinationFilePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error occurred while copying file: {Path.GetFileName(file)}\n\n{ex.Message}");
                        }
                    }
                }
                else
                {
                    listBoxInformationM.Items.Add("No missing files found with different extensions.");
                }

                // Get missing files count
                lblMissingCountM.Text = $"Missing Files with Different Extensions: {missingFiles.Count}";
            }
            else
            {
                MessageBox.Show("Please provide valid folder paths and destination folder.");
            }
            // Disable the transfer button
            buttonTransferM.Enabled = false;
            isTransferButtonDisabled = true;
        }

        private void buttonCsvCompareM_Click(object sender, EventArgs e)
        {
            string folder1 = textBoxFolder1M.Text;
            string folder2 = textBoxFolder2M.Text;
            string destinationFolder = textBoxDestinationFolderM.Text;

            if (Directory.Exists(folder1) && Directory.Exists(folder2) && Directory.Exists(destinationFolder))
            {
                Dictionary<string, string> folder1Files = GetAllFiles(folder1);
                Dictionary<string, string> folder2Files = GetAllFiles(folder2);

                // Missing .csv files list
                List<string> missingFiles = GetMissingDifferentExtensions(folder1Files, folder2Files);

                listBoxInformationM.Items.Clear();
                if (missingFiles.Count > 0)
                {
                    foreach (string file in missingFiles)
                    {
                        listBoxInformationM.Items.Add(file);
                        string sourceFilePath = Path.Combine(folder1, file);
                        string destinationFilePath = Path.Combine(destinationFolder, Path.GetFileName(file));

                        try
                        {
                            File.Copy(sourceFilePath, destinationFilePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error occurred while copying file: {Path.GetFileName(file)}\n\n{ex.Message}");
                        }
                    }
                }
                else
                {
                    listBoxInformationM.Items.Add("No missing .csv files found.");
                }

                // Get missing files count
                lblMissingCountM.Text = $"Missing .csv Files: {missingFiles.Count}";
            }
            else
            {
                MessageBox.Show("Please provide valid folder paths and destination folder.");
            }

            // Disable the transfer button
            buttonTransferM.Enabled = false;
            isTransferButtonDisabled = true;
        }

        private void btnCompareCapAdv_Click(object sender, EventArgs e)
        {
            string folderCap = textBoxFolder1M.Text;
            string folderAdv = textBoxFolder2M.Text;
            string destinationFolder = textBoxDestinationFolderM.Text;

            if (Directory.Exists(folderCap) && Directory.Exists(folderAdv))
            {
                Dictionary<string, string> capFiles = GetAllFilesWithExtension(folderCap, ".cap");
                Dictionary<string, string> advFiles = GetAllFilesWithExtension(folderAdv, ".adv");

                // Missing files list with same names
                List<string> missingFiles = GetMissingSameNames(capFiles, advFiles);

                listBoxInformationM.Items.Clear();
                if (missingFiles.Count > 0)
                {
                    foreach (string file in missingFiles)
                    {
                        listBoxInformationM.Items.Add(file);
                        string sourceFilePath = Path.Combine(folderCap, file);
                        string destinationFilePath = Path.Combine(destinationFolder, Path.GetFileName(file));

                        try
                        {
                            File.Copy(sourceFilePath, destinationFilePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error occurred while copying file: {Path.GetFileName(file)}\n\n{ex.Message}");
                        }
                    }
                }
                else
                {
                    listBoxInformationM.Items.Add("No missing files found with the same names.");
                }

                // Get missing files count
                lblMissingCountM.Text = $"Missing Files with Same Names: {missingFiles.Count}";
            }
            else
            {
                MessageBox.Show("Please provide valid folder paths.");
            }

            // Disable the transfer button
            buttonTransferM.Enabled = false;
            isTransferButtonDisabled = true;
        }

        // Get all files with a specific extension in a folder
        private Dictionary<string, string> GetAllFilesWithExtension(string folderPath, string extension)
        {
            Dictionary<string, string> files = new Dictionary<string, string>();

            foreach (string file in Directory.GetFiles(folderPath, "*" + extension))
            {
                string fileName = Path.GetFileName(file);
                string fullPath = Path.GetFullPath(file); // Get the full path of the file
                files[fileName] = fullPath;
            }

            foreach (string subfolder in Directory.GetDirectories(folderPath))
            {
                foreach (KeyValuePair<string, string> file in GetAllFilesWithExtension(subfolder, extension))
                {
                    files[file.Key] = file.Value;
                }
            }

            return files;
        }

        // Get missing files with the same names
        private List<string> GetMissingSameNames(Dictionary<string, string> folder1Files, Dictionary<string, string> folder2Files)
        {
            List<string> missingFiles = new List<string>();

            foreach (KeyValuePair<string, string> file in folder1Files)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file.Key);
                if (!folder2Files.ContainsKey(fileNameWithoutExtension + ".adv"))
                {
                    missingFiles.Add(file.Value);
                }
            }

            return missingFiles;
        }
    }
}
