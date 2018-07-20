using System;
using System.Reflection;

namespace ArchiverTool
{
    /// <summary>General helpers.</summary>
    public static class Common
    {
        /// <summary>
        /// Get information (name and version) from assembly.
        /// </summary>
        public static void AssemblyInfo()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyName assemblyName = assembly.GetName();
            string actualAssemblyName = assemblyName.Name;

            string actualAssemblyVersion = $"{actualAssemblyName} " +
                $"Version {assemblyName.Version.ToString()}";

            Console.Title = actualAssemblyVersion;
        }

        /// <summary>Logs a message to the console with time stamp.</summary>
        /// <param name="message">Message to be logged</param>
        public static void LogToConsole(string message) => Console.WriteLine(
            String.Concat(DateTime.Now.ToString("dd-MM-yy HH:mm:ss"), " ",
                message));

        /// <summary>Logs a message to the console with time stamp.</summary>
        /// <param name="message">Message to be logged, as a composite string</param>
        ///  <param name="args">Arguments for the composite</param>
        public static void LogToConsole(string message, params object[] args)
            => LogToConsole(String.Format(message, args));
    }
}
