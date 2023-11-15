using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Blood_Donation.Helpers
{
    public class codeExec: PageModel
    {
        public void ExecuteExe()
        {
            try
            {
                string exePath = @"C:\WINDOWS\system32\notepad.exe"; // Replace with the actual path to your executable

                // Create a new process start info
                ProcessStartInfo startInfo = new ProcessStartInfo(exePath);

                // You can specify command line arguments if your executable requires them
                // startInfo.Arguments = "argument1 argument2";

                // Redirect the standard output and error streams (if needed)
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;

                // Set up process details
                startInfo.UseShellExecute = false; // This is important to redirect output
                startInfo.CreateNoWindow = true; // This prevents a console window from appearing

                // Start the process
                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();

                // Optionally, capture the output for further processing
                string output = process.StandardOutput.ReadToEnd();
                string errorOutput = process.StandardError.ReadToEnd();

                // Close the process
                process.WaitForExit();
                process.Close();

                // You can use 'output' and 'errorOutput' as needed
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur
                // You should log the exception for debugging and security purposes
                // Handle the error as appropriate for your application
            }
        }
    }
}
