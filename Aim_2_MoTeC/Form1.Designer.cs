
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
            this.pathInput = new System.Windows.Forms.TextBox();
            this.browseButton1 = new System.Windows.Forms.Button();
            this.convertButton = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.DataLabel = new System.Windows.Forms.Label();
            this.readButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.convertWorker = new System.ComponentModel.BackgroundWorker();
            this.useRaw = new System.Windows.Forms.CheckBox();
            this.versionLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.renameBox = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.browseButton2 = new System.Windows.Forms.Button();
            this.pathOutput = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // pathInput
            // 
            this.pathInput.Location = new System.Drawing.Point(303, 74);
            this.pathInput.Name = "pathInput";
            this.pathInput.PlaceholderText = "path input";
            this.pathInput.Size = new System.Drawing.Size(455, 25);
            this.pathInput.TabIndex = 0;
            this.pathInput.TextChanged += new System.EventHandler(this.pathInput_TextChanged);
            // 
            // browseButton1
            // 
            this.browseButton1.Location = new System.Drawing.Point(146, 70);
            this.browseButton1.Name = "browseButton1";
            this.browseButton1.Size = new System.Drawing.Size(141, 30);
            this.browseButton1.TabIndex = 1;
            this.browseButton1.Text = "Browse path";
            this.browseButton1.UseVisualStyleBackColor = true;
            this.browseButton1.Click += new System.EventHandler(this.browseButton1_Click);
            // 
            // convertButton
            // 
            this.convertButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.convertButton.Location = new System.Drawing.Point(292, 487);
            this.convertButton.Name = "convertButton";
            this.convertButton.Size = new System.Drawing.Size(200, 50);
            this.convertButton.TabIndex = 2;
            this.convertButton.Text = "Convert";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.convertButton_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Title.Location = new System.Drawing.Point(318, 24);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(154, 30);
            this.Title.TabIndex = 3;
            this.Title.Text = "AIM to MoTeC";
            // 
            // DataLabel
            // 
            this.DataLabel.AutoSize = true;
            this.DataLabel.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DataLabel.Location = new System.Drawing.Point(22, 243);
            this.DataLabel.Name = "DataLabel";
            this.DataLabel.Size = new System.Drawing.Size(49, 15);
            this.DataLabel.TabIndex = 4;
            this.DataLabel.Text = "[DATA]";
            // 
            // readButton
            // 
            this.readButton.Location = new System.Drawing.Point(21, 178);
            this.readButton.Name = "readButton";
            this.readButton.Size = new System.Drawing.Size(200, 50);
            this.readButton.TabIndex = 5;
            this.readButton.Text = "Read Data";
            this.readButton.UseVisualStyleBackColor = true;
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar1.Location = new System.Drawing.Point(167, 452);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(450, 20);
            this.progressBar1.TabIndex = 6;
            // 
            // convertWorker
            // 
            this.convertWorker.WorkerReportsProgress = true;
            this.convertWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.convertWorker_DoWork);
            // 
            // useRaw
            // 
            this.useRaw.AutoSize = true;
            this.useRaw.Location = new System.Drawing.Point(327, 415);
            this.useRaw.Name = "useRaw";
            this.useRaw.Size = new System.Drawing.Size(131, 21);
            this.useRaw.TabIndex = 7;
            this.useRaw.Text = "Use raw GPS data";
            this.useRaw.UseVisualStyleBackColor = true;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(3, 535);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(69, 17);
            this.versionLabel.TabIndex = 8;
            this.versionLabel.Text = "[VERSION]";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(473, 169);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(99, 17);
            this.titleLabel.TabIndex = 9;
            this.titleLabel.Text = "Channel Names";
            // 
            // renameBox
            // 
            this.renameBox.AutoSize = true;
            this.renameBox.Location = new System.Drawing.Point(327, 388);
            this.renameBox.Name = "renameBox";
            this.renameBox.Size = new System.Drawing.Size(128, 21);
            this.renameBox.TabIndex = 12;
            this.renameBox.Text = "Rename channels";
            this.renameBox.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(303, 198);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(455, 174);
            this.listBox1.TabIndex = 13;
            // 
            // browseButton2
            // 
            this.browseButton2.Location = new System.Drawing.Point(146, 112);
            this.browseButton2.Name = "browseButton2";
            this.browseButton2.Size = new System.Drawing.Size(141, 30);
            this.browseButton2.TabIndex = 15;
            this.browseButton2.Text = "Browse path";
            this.browseButton2.UseVisualStyleBackColor = true;
            this.browseButton2.Click += new System.EventHandler(this.browseButton2_Click);
            // 
            // pathOutput
            // 
            this.pathOutput.Location = new System.Drawing.Point(303, 116);
            this.pathOutput.Name = "pathOutput";
            this.pathOutput.PlaceholderText = "path output (optionnal)";
            this.pathOutput.Size = new System.Drawing.Size(455, 25);
            this.pathOutput.TabIndex = 14;
            this.pathOutput.TextChanged += new System.EventHandler(this.pathOutput_TextChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(41, 74);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(88, 21);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "sub-folder";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.browseButton2);
            this.Controls.Add(this.pathOutput);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.renameBox);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.useRaw);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.readButton);
            this.Controls.Add(this.DataLabel);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.browseButton1);
            this.Controls.Add(this.pathInput);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aim to MoTeC i2 Converter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox pathInput;
        private System.Windows.Forms.Button browseButton1;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label DataLabel;
        private System.Windows.Forms.Button readButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.ComponentModel.BackgroundWorker convertWorker;
        private System.Windows.Forms.CheckBox useRaw;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.CheckBox renameBox;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button browseButton2;
        private System.Windows.Forms.TextBox pathOutput;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

