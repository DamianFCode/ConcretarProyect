using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Concretar.Helper.Extensions
{
    public static class LoggerExtension
    {
        /// <summary>
        /// Log exception details such as error line, error file or method with errors.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogErrorException(this ILogger logger, Exception ex, string message = null, params object[] args)
        {
            var trace = new System.Diagnostics.StackTrace(ex, true);
            var frame = trace.GetFrames().Last();
            var lineNumber = frame.GetFileLineNumber();
            var fileName = "[private]";
            var methodName = frame.GetMethod().Name;
            var description = "Error in line " + lineNumber.ToString() + " in file " + fileName + ". Detail: " + message + " - " + ex.ToString();
            logger.LogError(description, args);
            logger.LogTrace(ex, description, args);
            
        }
       
    }
}

