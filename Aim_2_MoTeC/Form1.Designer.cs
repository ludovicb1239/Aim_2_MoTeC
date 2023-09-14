
namespace Aim_2_MoTeC
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            PathInputText = new System.Windows.Forms.TextBox();
            BrowseInputButton = new System.Windows.Forms.Button();
            convertButton = new System.Windows.Forms.Button();
            Title = new System.Windows.Forms.Label();
            DataLabel = new System.Windows.Forms.Label();
            readButton = new System.Windows.Forms.Button();
            progressBar1 = new System.Windows.Forms.ProgressBar();
            convertWorker = new System.ComponentModel.BackgroundWorker();
            UseRawGPSCheckBox = new System.Windows.Forms.CheckBox();
            versionLabel = new System.Windows.Forms.Label();
            titleLabel = new System.Windows.Forms.Label();
            RenameChannelsCheckBox = new System.Windows.Forms.CheckBox();
            listBox1 = new System.Windows.Forms.ListBox();
            BrowseOutputButton = new System.Windows.Forms.Button();
            PathOutputText = new System.Windows.Forms.TextBox();
            SubFolderCheckBox = new System.Windows.Forms.CheckBox();
            SuspendLayout();
            // 
            // PathInputText
            // 
            PathInputText.Location = new System.Drawing.Point(303, 74);
            PathInputText.Name = "PathInputText";
            PathInputText.PlaceholderText = "path input";
            PathInputText.Size = new System.Drawing.Size(455, 25);
            PathInputText.TabIndex = 0;
            PathInputText.TextChanged += pathInput_TextChanged;
            // 
            // BrowseInputButton
            // 
            BrowseInputButton.Location = new System.Drawing.Point(146, 70);
            BrowseInputButton.Name = "BrowseInputButton";
            BrowseInputButton.Size = new System.Drawing.Size(141, 30);
            BrowseInputButton.TabIndex = 1;
            BrowseInputButton.Text = "Browse path";
            BrowseInputButton.UseVisualStyleBackColor = true;
            BrowseInputButton.Click += BrowseInputButton_Click;
            // 
            // convertButton
            // 
            convertButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            convertButton.Location = new System.Drawing.Point(292, 487);
            convertButton.Name = "convertButton";
            convertButton.Size = new System.Drawing.Size(200, 50);
            convertButton.TabIndex = 2;
            convertButton.Text = "Convert";
            convertButton.UseVisualStyleBackColor = true;
            convertButton.Click += ConvertButton_Click;
            // 
            // Title
            // 
            Title.AutoSize = true;
            Title.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Title.Location = new System.Drawing.Point(318, 24);
            Title.Name = "Title";
            Title.Size = new System.Drawing.Size(154, 30);
            Title.TabIndex = 3;
            Title.Text = "AIM to MoTeC";
            // 
            // DataLabel
            // 
            DataLabel.AutoSize = true;
            DataLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            DataLabel.Location = new System.Drawing.Point(22, 243);
            DataLabel.Name = "DataLabel";
            DataLabel.Size = new System.Drawing.Size(49, 15);
            DataLabel.TabIndex = 4;
            DataLabel.Text = "[DATA]";
            // 
            // readButton
            // 
            readButton.Location = new System.Drawing.Point(21, 178);
            readButton.Name = "readButton";
            readButton.Size = new System.Drawing.Size(200, 50);
            readButton.TabIndex = 5;
            readButton.Text = "Read Data";
            readButton.UseVisualStyleBackColor = true;
            readButton.Click += readButton_Click;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            progressBar1.Location = new System.Drawing.Point(167, 452);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(450, 20);
            progressBar1.TabIndex = 6;
            // 
            // convertWorker
            // 
            convertWorker.WorkerReportsProgress = true;
            convertWorker.DoWork += ConvertWorker_DoWork;
            // 
            // UseRawGPSCheckBox
            // 
            UseRawGPSCheckBox.AutoSize = true;
            UseRawGPSCheckBox.Location = new System.Drawing.Point(327, 415);
            UseRawGPSCheckBox.Name = "UseRawGPSCheckBox";
            UseRawGPSCheckBox.Size = new System.Drawing.Size(131, 21);
            UseRawGPSCheckBox.TabIndex = 7;
            UseRawGPSCheckBox.Text = "Use raw GPS data";
            UseRawGPSCheckBox.UseVisualStyleBackColor = true;
            // 
            // versionLabel
            // 
            versionLabel.AutoSize = true;
            versionLabel.Location = new System.Drawing.Point(3, 535);
            versionLabel.Name = "versionLabel";
            versionLabel.Size = new System.Drawing.Size(69, 17);
            versionLabel.TabIndex = 8;
            versionLabel.Text = "[VERSION]";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new System.Drawing.Point(473, 169);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new System.Drawing.Size(99, 17);
            titleLabel.TabIndex = 9;
            titleLabel.Text = "Channel Names";
            // 
            // RenameChannelsCheckBox
            // 
            RenameChannelsCheckBox.AutoSize = true;
            RenameChannelsCheckBox.Location = new System.Drawing.Point(327, 388);
            RenameChannelsCheckBox.Name = "RenameChannelsCheckBox";
            RenameChannelsCheckBox.Size = new System.Drawing.Size(128, 21);
            RenameChannelsCheckBox.TabIndex = 12;
            RenameChannelsCheckBox.Text = "Rename channels";
            RenameChannelsCheckBox.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 17;
            listBox1.Location = new System.Drawing.Point(303, 198);
            listBox1.Name = "listBox1";
            listBox1.Size = new System.Drawing.Size(455, 174);
            listBox1.TabIndex = 13;
            // 
            // BrowseOutputButton
            // 
            BrowseOutputButton.Location = new System.Drawing.Point(146, 112);
            BrowseOutputButton.Name = "BrowseOutputButton";
            BrowseOutputButton.Size = new System.Drawing.Size(141, 30);
            BrowseOutputButton.TabIndex = 15;
            BrowseOutputButton.Text = "Browse path";
            BrowseOutputButton.UseVisualStyleBackColor = true;
            BrowseOutputButton.Click += BrowseOutputButton_Click;
            // 
            // PathOutputText
            // 
            PathOutputText.Location = new System.Drawing.Point(303, 116);
            PathOutputText.Name = "PathOutputText";
            PathOutputText.PlaceholderText = "path output (optionnal)";
            PathOutputText.Size = new System.Drawing.Size(455, 25);
            PathOutputText.TabIndex = 14;
            PathOutputText.TextChanged += pathOutput_TextChanged;
            // 
            // SubFolderCheckBox
            // 
            SubFolderCheckBox.AutoSize = true;
            SubFolderCheckBox.Location = new System.Drawing.Point(41, 74);
            SubFolderCheckBox.Name = "SubFolderCheckBox";
            SubFolderCheckBox.Size = new System.Drawing.Size(88, 21);
            SubFolderCheckBox.TabIndex = 16;
            SubFolderCheckBox.Text = "sub-folder";
            SubFolderCheckBox.UseVisualStyleBackColor = true;
            SubFolderCheckBox.CheckedChanged += SubFolderCheckBox_CheckedChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(784, 561);
            Controls.Add(SubFolderCheckBox);
            Controls.Add(BrowseOutputButton);
            Controls.Add(PathOutputText);
            Controls.Add(listBox1);
            Controls.Add(RenameChannelsCheckBox);
            Controls.Add(titleLabel);
            Controls.Add(versionLabel);
            Controls.Add(UseRawGPSCheckBox);
            Controls.Add(progressBar1);
            Controls.Add(readButton);
            Controls.Add(DataLabel);
            Controls.Add(Title);
            Controls.Add(convertButton);
            Controls.Add(BrowseInputButton);
            Controls.Add(PathInputText);
            Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "Aim to MoTeC i2 Converter";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox PathInputText;
        private System.Windows.Forms.Button BrowseInputButton;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label DataLabel;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker convertWorker;
        private System.Windows.Forms.CheckBox UseRawGPSCheckBox;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.CheckBox RenameChannelsCheckBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button BrowseOutputButton;
        private System.Windows.Forms.TextBox PathOutputText;
        private System.Windows.Forms.CheckBox SubFolderCheckBox;
    }
}

