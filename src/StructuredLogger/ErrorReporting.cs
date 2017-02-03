﻿using System;
using System.IO;

namespace Microsoft.Build.Logging.StructuredLogger
{
    public class ErrorReporting
    {
        private static readonly string logFilePath = Path.Combine(GetRootPath(), "LoggerExceptions.txt");

        private static string GetRootPath()
        {
#if NET451
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
#else
            var path = Path.GetTempPath();
#endif
            path = Path.Combine(path, "Microsoft", "MSBuildStructuredLog");
            return path;
        }

        public static void ReportException(Exception ex)
        {
            if (ex == null)
            {
                return;
            }

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));

                // if the log has gotten too big, delete it
                if (File.Exists(logFilePath) && new FileInfo(logFilePath).Length > 10000000)
                {
                    File.Delete(logFilePath);
                }

                File.AppendAllText(logFilePath, ex.ToString());
            }
            catch (Exception)
            {
            }
        }
    }
}
