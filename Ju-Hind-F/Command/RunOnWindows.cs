using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace cali.Command
{
    internal class RunOnWindows
    {
        public static void RunPythonFile(List<string> commands)
        {
            // Ensure the commands list has at least two elements (Python path and script path)
            if (commands.Count < 2)
            {
                Console.WriteLine("Please provide both the Python path and the script path.");
                return;
            }

            string pythonPath = commands[0];  // Python executable path
            string scriptPath = commands[1];  // Python script path

            // Collect the remaining elements (arguments) as a string
            string args = string.Join(" ", commands.GetRange(2, commands.Count - 2));  // All arguments after the script path

            // Set up the process to execute the Python script with the given arguments
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = pythonPath,
                Arguments = $"{scriptPath} {args}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            try
            {
                // Start the process and capture the output and errors
                using (Process process = Process.Start(start))
                {
                    // Read the standard output stream
                    using (StreamReader reader = process.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine("\n" + result);
                    }

                    // Read the error stream if any errors occur
                    using (StreamReader reader = process.StandardError)
                    {
                        string errors = reader.ReadToEnd();
                        if (!string.IsNullOrEmpty(errors))
                        {
                            Console.WriteLine("\n" + errors);
                        }
                    }

                    // Ensure the process completes before continuing
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while running the command: " + ex.Message);
            }
        }
    }
}
