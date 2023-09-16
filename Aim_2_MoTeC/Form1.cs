using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace Aim_2_MoTeC
{
    public partial class Form1 : Form
    {
        private string inFilePath = "", outFilePath = "";
        private bool folderMode = false;
        private bool usesDarkTheme = true;
        public Form1()
        {
            InitializeComponent();
            updateButtons();
            convertWorker.ProgressChanged    += ConvertWorker_ProgressChanged;
            convertWorker.RunWorkerCompleted += ConvertWorker_RunWorkerCompleted;
            Application.ApplicationExit      += OnApplicationExit;

            // Get the assembly information of the current application
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);

            versionLabel.Text = "V" + fileVersionInfo.ProductVersion;

            // Load the previously saved boolean value from settings
            SubFolderCheckBox.Checked      = Properties.Settings.Default.S_UseSubSearch;
            folderMode                     = Properties.Settings.Default.S_UseSubSearch;
            RenameChannelsCheckBox.Checked = Properties.Settings.Default.S_RenameChannels;
            UseRawGPSCheckBox.Checked      = Properties.Settings.Default.S_UseRawGPS;
            usesDarkTheme                  = Properties.Settings.Default.S_UseDarkTheme;
            ThemeCheckBox.Checked          = Properties.Settings.Default.S_UseDarkTheme;

            CreateCustomTitleBar();
            UpdateTheme();
        }

        private void BrowseInputButton_Click(object sender, EventArgs e)
        {
            if (folderMode)
            {
                FolderBrowserDialog folderBrowserDialog = new();

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    inFilePath = folderBrowserDialog.SelectedPath;
                    PathInputText.Text = inFilePath;
                }
            }
            else
            {
                OpenFileDialog openFileDialog = new();
                openFileDialog.Filter = "DRK Files (*.drk)|*.drk";
                openFileDialog.Title = "Select a DRK File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    inFilePath = openFileDialog.FileName;
                    PathInputText.Text = inFilePath;
                }
            }
        }
        private void BrowseOutputButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                outFilePath = folderBrowserDialog.SelectedPath;
                PathOutputText.Text = outFilePath;
            }
        }

        private void ConvertButton_Click(object sender, EventArgs e)
        {
            // Start the worker to make it work in background
            convertWorker.RunWorkerAsync();
            updateButtons();
        }
        private void ConvertWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Worker started conversion");

            List<string> filePaths = Converter.getFileList(inFilePath, folderMode);

            bool usingRAW_GPS = UseRawGPSCheckBox.Checked, convertName = RenameChannelsCheckBox.Checked;

            foreach (string path in filePaths)
                Converter.Convert(path, usingRAW_GPS, convertName, (BackgroundWorker)sender);

            e.Result = "Done! File saved in " + (outFilePath == "" ? "the same folder as the drk file" : outFilePath);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private void ConvertWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("Conversion progress: " + e.ProgressPercentage + "%");

            progressBar1.Value = e.ProgressPercentage;
        }
        private void ConvertWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            convertWorker.Dispose();
            updateButtons();

            if (e.Error != null)
            {
                Console.WriteLine("An error occurred during conversion: " + e.Error.Message);
                MessageBox.Show("An error occurred:" + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                Console.WriteLine("Conversion was cancelled.");
                MessageBox.Show("Conversion was cancelled.", "Conversion result", MessageBoxButtons.OK);
            }
            else
            {
                Console.WriteLine("Conversion result: " + e.Result);
                MessageBox.Show("Conversion result: " + e.Result.ToString(), "Conversion result", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void readButton_Click(object sender, EventArgs e)
        {
            DataLabel.Text = "Reading Data...";
            DataLabel.Refresh();

            List<string> filePaths = Converter.getFileList(inFilePath, folderMode);

            List<string> data, names;
            Converter.Read(filePaths[0], UseRawGPSCheckBox.Checked, out data, out names);

            DataLabel.Text = string.Join("\n", data);

            listBox1.Items.Clear();
            foreach (string str in names)
                listBox1.Items.Add(str);
            listBox1.Refresh();
        }

        private void updateButtons()
        {
            bool pathOk = checkIfInputPathOK(inFilePath);
            convertButton.Enabled = pathOk && !convertWorker.IsBusy;
            readButton.Enabled = pathOk;
        }
        private bool checkIfInputPathOK(string input)
        {
            if (input == "") return false;
            if (!folderMode) return Path.GetExtension(input).ToLower() == ".drk";
            else return true;
        }

        private void SubFolderCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            folderMode = SubFolderCheckBox.Checked;
            if (folderMode)
            {
                inFilePath = Path.GetDirectoryName(inFilePath);
                PathInputText.Text = inFilePath;
            }
        }
        private void pathInput_TextChanged(object sender, EventArgs e)
        {
            inFilePath = PathInputText.Text;
            updateButtons();
        }
        private void pathOutput_TextChanged(object sender, EventArgs e)
        {
            outFilePath = PathOutputText.Text;
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            // Save any changes made by the user back to settings
            Properties.Settings.Default.S_UseSubSearch = SubFolderCheckBox.Checked;
            Properties.Settings.Default.S_RenameChannels = RenameChannelsCheckBox.Checked;
            Properties.Settings.Default.S_UseRawGPS = UseRawGPSCheckBox.Checked;
            Properties.Settings.Default.S_UseDarkTheme = usesDarkTheme;
            Properties.Settings.Default.Save();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, usesDarkTheme ? Color.Black : Color.Gray, ButtonBorderStyle.Solid);
        }
        private void CreateCustomTitleBar()
        {
            Panel customTitleBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30, // Adjust the height as needed
                BackColor = usesDarkTheme ? Color.FromArgb(37, 37, 38) : Color.WhiteSmoke,
            };

            // Add buttons, title label, or any other custom controls to the customTitleBar as needed.

            this.Controls.Add(customTitleBar);
            customTitleBar.MouseDown += customTitleBar_MouseDown;
            customTitleBar.MouseMove += customTitleBar_MouseMove;
            customTitleBar.MouseUp += customTitleBar_MouseUp;
        }
        private bool isDragging = false;
        private Point startPoint;

        private void customTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void customTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point endPoint = PointToScreen(new Point(e.X, e.Y));
                Location = new Point(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y);
            }
        }

        private void customTitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void ThemeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            usesDarkTheme = ThemeCheckBox.Checked;
            UpdateTheme();
        }
        private void UpdateTheme()
        {
            ChangeColor(this, usesDarkTheme);
            this.BackColor = usesDarkTheme ? Color.FromArgb(255, 37, 37, 38) : Color.WhiteSmoke;
        }
        private static void ChangeColor(Control control, bool dark)
        {
            control.ForeColor = dark ? Color.WhiteSmoke : Color.Black; // Set the text color for the current control

            if (control is ListBox or TextBox)
                control.BackColor = dark ? Color.FromArgb(255, 30, 30, 30) : Color.White;

            if (control is Button button)
            {
                button.FlatAppearance.BorderColor = Color.Gray;
                button.FlatAppearance.MouseDownBackColor = dark ? Color.FromArgb(62, 62, 66) : Color.Silver;
                button.FlatAppearance.MouseOverBackColor = dark ? Color.FromArgb(75, 75, 80) : Color.Gainsboro;
            }
            if (control is CheckBox check)
            {
                check.BackColor = dark ? Color.FromArgb(37, 37, 38) : Color.WhiteSmoke;
                if (check.Appearance == Appearance.Button)
                {
                    check.FlatAppearance.BorderColor = Color.Gray;
                    check.FlatAppearance.MouseDownBackColor = dark ? Color.FromArgb(62, 62, 66) : Color.Silver;
                    check.FlatAppearance.MouseOverBackColor = dark ? Color.FromArgb(75, 75, 80) : Color.Gainsboro;

                }
            }
            if (control is Panel panel)
            {
                panel.BackColor = dark ? Color.FromArgb(255, 37, 37, 38) : Color.WhiteSmoke;
            }

            // Recursively apply the text color change to child controls
            foreach (Control childControl in control.Controls) ChangeColor(childControl, dark);
        }
    }
}