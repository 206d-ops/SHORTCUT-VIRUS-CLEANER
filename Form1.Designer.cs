namespace ShortcutCleaner
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 450); // Made window wider
            this.Text = "Shortcut Virus Cleaner";

            // Initialize Controls
            this.comboBoxDrives = new System.Windows.Forms.ComboBox();
            this.buttonClean = new System.Windows.Forms.Button();
            this.buttonDeepClean = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();

            // ComboBox
            this.comboBoxDrives.Location = new System.Drawing.Point(30, 30);
            this.comboBoxDrives.Width = 100;
            this.Controls.Add(this.comboBoxDrives);

            // Original Clean Button
            this.buttonClean.Text = "Clean Shortcuts";
            this.buttonClean.Location = new System.Drawing.Point(150, 28);
            this.buttonClean.Width = 120;
            this.buttonClean.Click += new System.EventHandler(this.buttonClean_Click);
            this.Controls.Add(this.buttonClean);

            // NEW Deep Clean Button
            this.buttonDeepClean.Text = "Deep Clean (Unhide & Fix)";
            this.buttonDeepClean.Location = new System.Drawing.Point(280, 28);
            this.buttonDeepClean.Width = 180;
            this.buttonDeepClean.Click += new System.EventHandler(this.buttonDeepClean_Click);
            this.Controls.Add(this.buttonDeepClean);

            // TextBox Log
            this.textBoxLog.Location = new System.Drawing.Point(30, 70);
            this.textBoxLog.Width = 480; // Made log wider
            this.textBoxLog.Height = 300;
            this.textBoxLog.Multiline = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.ReadOnly = true; // User cannot type in the log
            this.Controls.Add(this.textBoxLog);
        }

        #endregion

        // Declare the variables for our controls
        private System.Windows.Forms.ComboBox comboBoxDrives;
        private System.Windows.Forms.Button buttonClean;
        private System.Windows.Forms.Button buttonDeepClean;
        private System.Windows.Forms.TextBox textBoxLog;
    }
}