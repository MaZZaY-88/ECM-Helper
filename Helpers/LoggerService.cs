using System;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ECMHelper
{
    public static class LoggerService
    {
        /// <summary>
        /// Configures the Serilog logger using settings from the configuration file.
        /// </summary>
        /// <param name="configuration">Configuration object to read settings from (e.g., appsettings.json).</param>
        public static void ConfigureLogger(IConfiguration configuration)
        {
            // Retrieve the log file path from the configuration. If not set, default to "logs/app.log".
            var logFilePath = configuration["Logging:LogFilePath"];

            // Retrieve the rolling interval for the log file. Default to daily if not specified.
            var rollingInterval = Enum.TryParse(configuration["Logging:RollingInterval"], out RollingInterval interval) ? interval : RollingInterval.Day;

            // Configure the logger to write logs to the specified file with the given rolling interval.
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(logFilePath ?? "logs/app.log", rollingInterval: rollingInterval)
                .CreateLogger();
        }

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        /// <param name="message">The message to log as information.</param>
        public static void LogInfo(string message)
        {
            // Write an informational log entry.
            Log.Information(message);
        }

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="message">The message to log as a warning.</param>
        public static void LogWarning(string message)
        {
            // Write a warning log entry.
            Log.Warning(message);
        }

        /// <summary>
        /// Logs an error message along with an exception.
        /// </summary>
        /// <param name="message">The message to log as an error.</param>
        /// <param name="ex">The exception to include in the error log.</param>
        public static void LogError(string message, Exception ex)
        {
            // Write an error log entry, including the exception details.
            Log.Error(ex, message);
        }
    }
}
