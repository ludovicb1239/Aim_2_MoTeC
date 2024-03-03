using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;

namespace Aim_2_MoTeC
{
    public partial class Form1 : Form
    {
        private string inFilePath = "", outFilePath = "";
        private bool folderMode = false;
        private bool usesDarkTheme = true;
        /// <summary>
        /// Constructor for Form1, the main form of the application.
        /// Initializes the form and sets up event handlers. Also, retrieves and displays
        /// the application's version information, loads saved settings, and creates a custom title bar.
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            CreateCustomTitleBar();
            InitializeCloseButton();
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

            UpdateTheme();

            try
            {
                Console.WriteLine("Library date: " + XRK.GetLibraryDate());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Testing the XRK Lib failed ! You should contact Ludovic to tell him");
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Event handler for the "Browse" button click event.
        /// Opens a file dialog or folder browser dialog based on the selected mode (folder or file).
        /// Updates the 'inFilePath' variable and the 'PathInputText' control accordingly.
        /// </summary>
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
                OpenFileDialog openFileDialog = new()
                {
                    Filter = "DRK Files (*.drk)|*.drk|XRK Files (*.xrk)|*.xrk",
                    Title = "Select a DRK or XRK File"
                };

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    inFilePath = openFileDialog.FileName;
                    PathInputText.Text = inFilePath;
                }
            }
        }
        /// <summary>
        /// Event handler for the "Browse Output" button click event.
        /// Opens a folder browser dialog to select an output directory and updates
        /// the 'outFilePath' variable and the 'PathOutputText' control with the selected path.
        /// </summary>
        private void BrowseOutputButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                outFilePath = folderBrowserDialog.SelectedPath;
                PathOutputText.Text = outFilePath;
            }
        }
        /// <summary>
        /// Event handler for the "Convert" button click event.
        /// Initiates the background worker to perform conversion tasks asynchronously.
        /// Updates the state of buttons using the 'updateButtons' method.
        /// </summary>
        private void ConvertButton_Click(object sender, EventArgs e)
        {
            // Start the worker to make it work in background
            convertWorker.RunWorkerAsync();
            updateButtons();
        }
        /// <summary>
        /// Event handler for the background worker's "DoWork" event.
        /// Initiates the conversion process in the background, using parameters such as
        /// the input file path, conversion options, and the background worker itself
        /// for progress reporting.
        /// </summary>
        private void ConvertWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Console.WriteLine("Worker started conversion");

            List<string> filePaths = Converter.getFileList(inFilePath, folderMode);

            bool usingRAW_GPS = UseRawGPSCheckBox.Checked, convertName = RenameChannelsCheckBox.Checked;

            foreach (string path in filePaths)
                Converter.Convert(path, outFilePath, usingRAW_GPS, convertName, (BackgroundWorker)sender);

            e.Result = "Done! File saved in " + (outFilePath == "" ? "the same folder as the drk file" : outFilePath);

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        /// <summary>
        /// Event handler for the background worker's "ProgressChanged" event.
        /// Updates the progress of the conversion process and the associated progress bar control.
        /// </summary>
        private void ConvertWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("Conversion progress: " + e.ProgressPercentage + "%");

            progressBar1.Value = e.ProgressPercentage;
        }
        /// <summary>
        /// Event handler for the background worker's "RunWorkerCompleted" event.
        /// Performs cleanup, updates the state of buttons, and displays a message box
        /// with the outcome of the conversion process (success, error, or cancellation).
        /// </summary>
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
        /// <summary>
        /// Event handler for the "Read" button click event.
        /// Initiates the process of reading data from a file or first file of folder.
        /// Displays a "Reading Data..." message, reads data, and updates UI controls with the result.
        /// </summary>
        private void readButton_Click(object sender, EventArgs e)
        {
            DataLabel.Text = "Reading Data...";
            DataLabel.Refresh();
            listBox1.Items.Clear();

            List<string> filePaths = Converter.getFileList(inFilePath, folderMode);

            Converter.Read(filePaths[0], UseRawGPSCheckBox.Checked, out List<string> data, out List<string> names);

            DataLabel.Text = string.Join("\n", data);

            foreach (string str in names)
                listBox1.Items.Add(str);
            listBox1.Refresh();
        }
        /// <summary>
        /// Updates the state (enabled or disabled) of certain buttons based on the current
        /// status of the input file path and the background worker.
        /// </summary>
        private void updateButtons()
        {
            bool pathOk = checkIfInputPathOK(inFilePath);
            convertButton.Enabled = pathOk && !convertWorker.IsBusy;
            readButton.Enabled = pathOk;
        }
        private bool checkIfInputPathOK(string input)
        {
            if (input == "") return false;
            if (!folderMode) return Path.GetExtension(input).ToLower() == ".drk" || Path.GetExtension(input).ToLower() == ".xrk";
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
        /// <summary>
        /// Event handler for the application's exit event.
        /// Saves user-specific settings
        /// </summary>
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
            Panel customTitleBar = new()
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

        private void InitializeCloseButton()
        {
            Label closeButton;
            // Create a Label control for the close button
            closeButton = new Label();
            closeButton.Text = "x";
            closeButton.Font = new Font("Arial", 14F, FontStyle.Regular);
            closeButton.BackColor = Color.Transparent;
            closeButton.AutoSize = true;
            closeButton.Cursor = Cursors.Hand;
            closeButton.Location = new Point(this.Width - 25, 2);

            // Attach a click event handler to the close button
            closeButton.Click += CloseButton_Click;

            // Add the close button to the form's Controls collection
            this.Controls.Add(closeButton);
            // Bring the close button to the front of the Z-order
            closeButton.BringToFront();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            // Close the form when the close button is clicked
            this.Close();
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