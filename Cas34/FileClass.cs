using System;
using System.IO;

namespace Cas34
{
    class FileClass
    {
        public static string LogFileName = "C:\\Kurs\\Cas34.log";

        static public void Log(string LogMessage)
        {
            // Write a log file to the specified location, if file exists, append text to the end
            using (StreamWriter file = new StreamWriter(LogFileName, true))
            {
                file.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss K"));
                file.WriteLine(LogMessage);
                file.WriteLine("**********");
                file.WriteLine();
            }
        }
    }
}