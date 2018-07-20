using System;
using System.Linq;
namespace ArchiverTool
{
    class Program
    {
        /// <summary>App exit code : Okay (no errors).</summary>
        const int ExitCode_OK = 0;

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            Common.AssemblyInfo();

            FileProcessor.Source = @"C:\MyFiles";
            FileProcessor.Destination = @"C:\MyDest";
            var process = new FileProcessor();

            // Process each file to output creation date
            string destinationBuildDir = String.Empty;
            foreach (var filePath in process.FilePaths)
            {
                // Build and create destination path
                destinationBuildDir = process.SplitToDestination(filePath);

                // Copy from source to grouped destination dir
                process.CopyFiles(filePath, destinationBuildDir);
            }

            Console.WriteLine("\nKey Return to exit.");
            Console.ReadLine();

            Environment.ExitCode = ExitCode_OK;
        }
    }
}
