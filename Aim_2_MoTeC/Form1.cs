using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Aim_2_MoTeC
{
    public partial class Form1 : Form
    {
        string filePath = "";
        string outFilePath = "";
        bool folderMode = false;
        public Form1()
        {
            InitializeComponent();
            updateButtons();
            convertWorker.ProgressChanged += ConvertWorker_ProgressChanged;
            convertWorker.RunWorkerCompleted += ConvertWorker_RunWorkerCompleted;

            // Get the assembly information of the current application
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            versionLabel.Text = "V" + fileVersionInfo.ProductVersion;
        }

        private void browseButton1_Click(object sender, EventArgs e)
        {
            if (folderMode)
            {
                FolderBrowserDialog folderBrowserDialog = new();

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = folderBrowserDialog.SelectedPath;
                    pathInput.Text = filePath;
                }
            }
            else
            {
                OpenFileDialog openFileDialog = new();
                openFileDialog.Filter = "DRK Files (*.drk)|*.drk";
                openFileDialog.Title = "Select a DRK File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    pathInput.Text = filePath;
                }
            }
        }
        private void browseButton2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                outFilePath = folderBrowserDialog.SelectedPath;
                pathOutput.Text = outFilePath;
            }
        }

        private void convertWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;

            List<string> filePaths = new();

            if (folderMode)
                SearchForDrkFiles(filePath, filePaths);
            else
                filePaths.Add(filePath);

            ChannelNamesConvert nameConverter = new ChannelNamesConvert();

            foreach (string path in filePaths)
            {
                if (!getID(path, out int id)) throw new Exception("Failed to get ID using dll");

                bool usingRAW_GPS = useRaw.Checked;
                bool convertName = renameBox.Checked;

                DataLog data_log = new();
                data_log.nameConverter = nameConverter;
                data_log.fromXRK(id, worker, usingRAW_GPS, convertName);

                if (data_log.channels.Count == 0) throw new Exception("Failed to find any channels in log data");

                DateTimeStruct dateTime = XRK.GetDateAndTime(id);

                MotecLog motec_log = new();
                motec_log.driver = XRK.GetRacerName(id);
                motec_log.vehicle_id = XRK.GetVehiculeName(id);
                motec_log.venue_name = XRK.GetTrackName(id);
                motec_log.event_session = XRK.GetVenueTypeName(id);
                motec_log.short_comment = XRK.GetChampionshipName(id);
                motec_log.datetime = dateTime;

                XRK.CloseFile(path);

                motec_log.Initialize();
                motec_log.AddDataLog(data_log);

                int year = dateTime.tm_year + 1900;
                int month = dateTime.tm_mon + 1;
                int day = dateTime.tm_mday;
                int hour = dateTime.tm_hour;
                int minute = dateTime.tm_min;
                int second = dateTime.tm_sec;

                Debug.WriteLine("Saving MoTeC log...");
                string directoryPath = outFilePath == "" ? Path.GetDirectoryName(path) : outFilePath;
                string newFileName = string.Format("{0}{1:D2}{2:D2}-{3:D2}{4:D2}{5:D2}.ld", year, month, day, hour, minute, second);
                string newFileNameExtra = string.Format("{0}{1:D2}{2:D2}-{3:D2}{4:D2}{5:D2}.ldx", year, month, day, hour, minute, second);
                string newFilePath = Path.Combine(directoryPath, newFileName);
                string newFilePathExtra = Path.Combine(directoryPath, newFileNameExtra);

                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                {
                    Debug.WriteLine("Directory '{0}' does not exist, will create it", directoryPath);
                    Directory.CreateDirectory(directoryPath);
                }

                motec_log.Write(newFilePath, newFilePathExtra);
                data_log.Clear();
            }

            e.Result = "Done! File saved in " + (outFilePath == "" ? "the same folder as the drk file" : outFilePath);
        }
        private void ConvertWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Debug.WriteLine("Conversion progress: " + e.ProgressPercentage + "%");

            progressBar1.Value = e.ProgressPercentage;
        }
        private void convertButton_Click(object sender, EventArgs e)
        {
            // Start the worker to make it work in background
            convertWorker.RunWorkerAsync();
            updateButtons();
        }
        private void ConvertWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            convertWorker.Dispose();
            updateButtons();

            if (e.Error != null)
            {
                Debug.WriteLine("An error occurred during conversion: " + e.Error.Message);
                MessageBox.Show("An error occurred:" + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                Debug.WriteLine("Conversion was cancelled.");
                MessageBox.Show("Conversion was cancelled.", "Conversion result", MessageBoxButtons.OK);
            }
            else
            {
                Debug.WriteLine("Conversion result: " + e.Result);
                MessageBox.Show("Conversion result: " + e.Result.ToString(), "Conversion result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            DataLabel.Text = "Reading Data...";
            DataLabel.Refresh();

            List<string> filePaths = new();

            if (folderMode)
                SearchForDrkFiles(filePath, filePaths);
            else
                filePaths.Add(filePath);

            if (!getID(filePaths[0], out int id)) return;

            List<string> info = new();
            info.Add("Vehicule name:     " + XRK.GetVehiculeName(id));
            info.Add("Track name:        " + XRK.GetTrackName(id));
            info.Add("Racer name:        " + XRK.GetRacerName(id));
            info.Add("Championship name: " + XRK.GetChampionshipName(id));
            info.Add("Venue type name:   " + XRK.GetVenueTypeName(id));

            DateTimeStruct dateTime = XRK.GetDateAndTime(id);

            int year = dateTime.tm_year + 1900;
            int month = dateTime.tm_mon + 1;
            int day = dateTime.tm_mday;
            int hour = dateTime.tm_hour;
            int minute = dateTime.tm_min;
            int second = dateTime.tm_sec;
            info.Add(string.Format("Date:              {0}-{1:D2}-{2:D2}", year, month, day));
            info.Add(string.Format("Time:              {0:D2}:{1:D2}:{2:D2}", hour, minute, second));

            int lapsCount = XRK.GetLapsCount(id);
            info.Add("Laps count:        " + lapsCount);


            DataLabel.Text = string.Join("\n", info.ToArray());


            ChannelNamesConvert nameConverter = new ChannelNamesConvert();

            listBox1.Items.Clear();
            int channelCount = XRK.GetChannelsCount(id);
            listBox1.Items.Add("-- From --" + new string('\t', 2) + "-- Rename --");
            for (int c = 0; c < channelCount; c++)
            {
                string name = XRK.GetChannelName(id, c);
                if (nameConverter.containsName(name, out nameConvert convert))
                    listBox1.Items.Add(name + new string('\t', 4 - ((name.Length + 1) / 4)) + "--> " + convert.to);
                else
                    listBox1.Items.Add(name);
            }
            int gpsChannelCount = useRaw.Checked ? XRK.GetGPSRawChannelsCount(id) : XRK.GetGPSChannelsCount(id);
            listBox1.Items.Add("-- GPS --" + new string('\t', 3) + "-- Rename --");
            for (int c = 0; c < gpsChannelCount; c++)
            {
                string name = useRaw.Checked ? XRK.GetGPSRawChannelName(id, c) : XRK.GetGPSChannelName(id, c);
                if (nameConverter.containsName(name, out nameConvert convert))
                    listBox1.Items.Add(name + new string('\t', 4 - ((name.Length + 1) / 4)) + "--> " + convert.to);
                else
                    listBox1.Items.Add(name);
            }
            XRK.CloseFile(filePaths[0]);
            listBox1.Refresh();
        }
        public static bool getID(string path, out int id)
        {
            try
            {
                id = XRK.OpenFile(path);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load MatLabXRK-2017-64-ReleaseU.dll", "Error", MessageBoxButtons.OK);

                Debug.WriteLine("Failed to load MatLabXRK-2017-64-ReleaseU.dll");
                Debug.WriteLine(ex.Message);
                id = -1;
                return false;
            }
        }

        void updateButtons()
        {
            bool pathOk = checkIfInputPathOK(filePath);
            convertButton.Enabled = pathOk && !convertWorker.IsBusy;
            readButton.Enabled = pathOk;
        }
        bool checkIfInputPathOK(string input)
        {
            if (input == "") return false;
            if (!folderMode) return Path.GetExtension(input).ToLower() == ".drk";
            else return true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            folderMode = checkBox1.Checked;
            if (folderMode)
            {
                filePath = Path.GetDirectoryName(filePath);
                pathInput.Text = filePath;
            }
        }
        private void pathInput_TextChanged(object sender, EventArgs e)
        {
            filePath = pathInput.Text;
            updateButtons();
        }
        private void pathOutput_TextChanged(object sender, EventArgs e)
        {
            outFilePath = pathOutput.Text;
        }
        static void SearchForDrkFiles(string directoryPath, List<string> paths)
        {
            try
            {
                // Search for .drk files in the current directory
                paths.AddRange(Directory.GetFiles(directoryPath, "*.drk"));

                // Recursively search subdirectories
                string[] subdirectories = Directory.GetDirectories(directoryPath);

                foreach (string subdirectory in subdirectories)
                    SearchForDrkFiles(subdirectory, paths);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}