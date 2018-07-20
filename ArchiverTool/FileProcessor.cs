using System;
using System.IO;

namespace ArchiverTool
{
    class FileProcessor
    {
        /// <summary>App exit code : Source directory does not exist.</summary>
        const int ExitCode_SourceDirEmpty = -1;

        /// <summary>App exit code : Source folder empty.</summary>
        const int ExitCode_SourceEmpty = -2;

        public static string Source;
        public static string Destination;
        private static string[] filePaths;

        // A read-only static property:
        public string[] FilePaths
        {
            get { return filePaths; }
        }

        public FileProcessor()
        {
            // Check if source directory exists
            if (!Directory.Exists(Source))
            {
                Common.LogToConsole($"ERROR: Source directory does not exist");
                Environment.ExitCode = ExitCode_SourceDirEmpty;
                return;
            }

            // Check if folder has files
            if (Directory.GetFiles(Source).Length == 0)
            {
                Common.LogToConsole($"ERROR: Folder {Source} is empty. Nothing to Copy.");
                Environment.ExitCode = ExitCode_SourceEmpty;
                return;
            }
            Common.LogToConsole($"Source directory: {Source}");

            // Get paths for all files, save to holding area.
            filePaths = Directory.GetFiles(Source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public string SplitToDestination(string filePath)
        {
            DateTime creationDate;
            int creationMonth = 0;
            int creationYear = 0;

            creationDate = File.GetCreationTime(filePath);
            creationMonth = creationDate.Month;
            creationYear = creationDate.Year;

            string subQuarterDir = GroupFiles(creationMonth);

            // Build and create destination path
            string destinationBuildDir = Path.Combine(Destination, creationYear.ToString(), subQuarterDir);
            Directory.CreateDirectory(destinationBuildDir);

            return destinationBuildDir;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="creationMonth"></param>
        /// <returns></returns>
        private string GroupFiles(int creationMonth)
        {
            string subQuarterDir = String.Empty;

            // Sort into these new sub dirs
            switch (creationMonth)
            {
                case 1:
                case 2:
                case 3:
                    subQuarterDir = "Q1";
                    break;
                case 4:
                case 5:
                case 6:
                    subQuarterDir = "Q2";
                    break;
                case 7:
                case 8:
                case 9:
                    subQuarterDir = "Q3";
                    break;
                case 10:
                case 11:
                case 12:
                    subQuarterDir = "Q4";
                    break;
                default:
                    Console.WriteLine("ERROR: File {0} month read error.");
                    break;
            }
            return subQuarterDir;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="destinationBuildDir"></param>
        public void CopyFiles(string filePath, string destinationBuildDir)
        {
            string fileName = Path.GetFileName(filePath);
            string copyFileName = Path.Combine(destinationBuildDir, fileName);

            File.Copy(filePath, copyFileName, overwrite: true);

            // Verify if file has been copied:
            if (File.Exists(copyFileName))
            {
                Console.WriteLine("Copied {0} to {1} ({2}) ", filePath, destinationBuildDir,
                    Convert.ToDateTime(File.GetCreationTime(filePath)).ToString("dd-MM-yyyy"));
            }
            else
            {
                Console.WriteLine("ERROR: File {0} copy error. ", copyFileName);
            }
        }
    }
}
