using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace AuctionBot.Code.Tool.LogUtils
{
    /// <summary>
    /// 用来格式化输出的控制台Log的通用代码
    /// by 饼干（摸了
    /// </summary>
    public class ConsoleLog
    {
        #region 控制台调用函数

        /// <summary>
        /// 打开控制台
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AllocConsole();

        #endregion 控制台调用函数

        #region Log等级设置

        private static LogLevel Level = LogLevel.Info;

        /// <summary>
        /// 设置日志等级
        /// </summary>
        /// <param name="level">LogLevel</param>
        public static void SetLogLevel(LogLevel level) => Level = level;

        #endregion Log等级设置

        #region 格式化错误Log

        public static string ErrorLogBuilder(Exception e)
        {
            StringBuilder errorMessageBuilder = new StringBuilder();
            errorMessageBuilder.Append("\r\n");
            errorMessageBuilder.Append("==============ERROR==============\r\n");
            errorMessageBuilder.Append("Error:");
            errorMessageBuilder.Append(e.GetType().FullName);
            errorMessageBuilder.Append("\r\n\r\n");
            errorMessageBuilder.Append("Message:");
            errorMessageBuilder.Append(e.Message);
            errorMessageBuilder.Append("\r\n\r\n");
            errorMessageBuilder.Append("Stack Trace:\r\n");
            errorMessageBuilder.Append(e.StackTrace);
            errorMessageBuilder.Append("\r\n");
            errorMessageBuilder.Append("=================================\r\n");
            return errorMessageBuilder.ToString();
        }

        #endregion 格式化错误Log

        #region 格式化控制台Log函数

        /// <summary>
        /// 向控制台发送Info信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Info(object type, object message)
        {
            if (Level != LogLevel.Error || Level != LogLevel.Warning)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"[SuiseiBot][{DateTime.Now}][INFO][{type}]{message}");
            }
        }

        /// <summary>
        /// 向控制台发送Warning信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Warning(object type, object message)
        {
            if (Level != LogLevel.Error)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"[SuiseiBot][{DateTime.Now}][");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("WARNINIG");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"][{type}]");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// 向控制台发送Error信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Error(object type, object message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"[SuiseiBot][{DateTime.Now}][");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("ERROR");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"][{type}]");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// 向控制台发送Fatal信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Fatal(object type, object message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"[SuiseiBot][{DateTime.Now}][");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("FATAL");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"][{type}]");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// 向控制台发送Debug信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Verbose(object type, object message)
        {
            if (Level == LogLevel.Debug)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"[SuiseiBot][{DateTime.Now}][");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Verbose");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"][{type}]");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        /// <summary>
        /// 向控制台发送Debug信息
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="message">信息内容</param>
        public static void Debug(object type, object message)
        {
            if (Level == LogLevel.Debug)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"[SuiseiBot][{DateTime.Now}][");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("DEBUG");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"][{type}]");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        #endregion 格式化控制台Log函数

        #region 全局错误Log

        /// <summary>
        /// 全局错误Log
        /// </summary>
        /// <param name="e"></param>
        public static void UnhandledExceptionLog(Exception e)
        {
            Fatal("UnhandledException", ErrorLogBuilder(e));
            Thread.Sleep(5000);
            Environment.Exit(0);
        }

        #endregion 全局错误Log
    }
}