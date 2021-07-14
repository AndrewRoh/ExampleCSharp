using System;
using System.Diagnostics;

namespace CSarpEx.Ex08
{
    public interface ILogger
    {
        void Log(string message);

        void Log(Exception ex) => Log(ex.Message);
        void Log(string logType, string msg)
        {
            if (logType == "Error" ||
                logType == "Warning" ||
                logType == "Info")
            {
                Log($"{logType}: {msg}");
            }
            else
            {
                throw new AccessViolationException("Invalid LogType");
            }
        }
    }

    class MyLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Debug.WriteLine(message);
        }

        public void Log(Exception ex)
        {
            Debug.WriteLine(ex.ToString());
        }
    }
    class Ex08_1
    {
        static void Main08_1()
        {
            MyLogger myLogger = new();
            myLogger.Log("Invalid data");   // logger.Log("Error", "Invalid data"); 에러

            ILogger logger = new MyLogger();
            logger.Log("Error", "Invalid data");
        }

    }
}
