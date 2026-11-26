using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ShortcutCleaner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadDrives(); // Load drives when the app starts
        }

        // Finds all drives and puts them in the dropdown
        private void LoadDrives()
        {
            comboBoxDrives.Items.Clear();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady && (d.DriveType == DriveType.Removable || d.DriveType == DriveType.Fixed))
                {
                    comboBoxDrives.Items.Add(d.Name); // e.g., "E:\"
                }
            }
            if (comboBoxDrives.Items.Count > 0)
                comboBoxDrives.SelectedIndex = 0;
        }

        // --- ORIGINAL CLEAN BUTTON LOGIC ---
        private void buttonClean_Click(object sender, EventArgs e)
        {
            if (comboBoxDrives.SelectedItem == null)
            {
                textBoxLog.AppendText("Please select a drive first.\r\n");
                return;
            }

            string drive = comboBoxDrives.SelectedItem.ToString();
            textBoxLog.AppendText($"\r\nStarting basic cleanup on {drive}...\r\n");

            string attribCommand = $"/c attrib -r -h -s /s /d {drive}*.*";
            string delCommand = $"/c del /q {drive}*.lnk";

            try
            {
                RunCommand(attribCommand, "Unhiding files...");
                RunCommand(delCommand, "Deleting shortcut files (.lnk)...");
                textBoxLog.AppendText("Basic cleanup complete.\r\n");
            }
            catch (Exception ex)
            {
                textBoxLog.AppendText($"Error: {ex.Message}\r\n");
            }
        }

        // --- NEW DEEP CLEAN BUTTON LOGIC ---
        private void buttonDeepClean_Click(object sender, EventArgs e)
        {
            if (comboBoxDrives.SelectedItem == null)
            {
                textBoxLog.AppendText("Please select a drive first.\r\n");
                return;
            }

            string drive = comboBoxDrives.SelectedItem.ToString();
            textBoxLog.AppendText($"\r\n--- Starting Deep Clean on {drive} ---\r\n");

            // 1. Unhide Folders (The /d flag is crucial for directories)
            string attribFoldersCmd = $"/c attrib -r -h -s /s /d {drive}*.*";
            RunCommand(attribFoldersCmd, "Unhiding all files and folders...");

            // 2. Delete shortcuts
            RunCommand($"/c del /q {drive}*.lnk", "Deleting shortcut files...");

            // 3. Remove autorun.inf (The file that auto-launches the virus)
            string autorunPath = System.IO.Path.Combine(drive, "autorun.inf");
            textBoxLog.AppendText("Checking for autorun.inf...\r\n");

            if (System.IO.File.Exists(autorunPath))
            {
                try
                {
                    RunCommand($"/c attrib -r -h -s \"{autorunPath}\"", "Removing protection from autorun.inf...");
                    System.IO.File.Delete(autorunPath);
                    textBoxLog.AppendText("SUCCESS: autorun.inf virus file deleted!\r\n");
                }
                catch (Exception ex)
                {
                    textBoxLog.AppendText($"Failed to delete autorun.inf: {ex.Message}\r\n");
                }
            }
            else
            {
                textBoxLog.AppendText("No autorun.inf found. Drive is safe from auto-run viruses.\r\n");
            }

            textBoxLog.AppendText("--- Deep Clean Complete ---\r\n");
        }

        // --- HELPER METHOD TO RUN COMMANDS ---
        private void RunCommand(string command, string message)
        {
            textBoxLog.AppendText(message + "\r\n");
            
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = command,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                // Only show output if it exists, and ignore the harmless "Could Not Find" error
                if (!string.IsNullOrEmpty(output) && !output.Contains("Could Not Find"))
                    textBoxLog.AppendText(output + "\r\n");
                
                if (!string.IsNullOrEmpty(error) && !error.Contains("Could Not Find"))
                    textBoxLog.AppendText("Error: " + error + "\r\n");
            }
        }
    }
}