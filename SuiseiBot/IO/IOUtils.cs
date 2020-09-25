using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Text;
using AuctionBot.Code.Tool.LogUtils;

namespace AuctionBot.Code.IO
{
    internal static class IOUtils
    {
        #region IO工具

        /// <summary>
        /// 获取错误报告路径
        /// </summary>
        public static string GetCrashLogPath()
        {
            string path = $@"{Directory.GetCurrentDirectory()}\crash".Replace('\\', '/');
            //检查目录是否存在，不存在则新建一个
            Directory.CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// 获取应用配置文件的绝对路径
        /// </summary>
        public static string GetConfigPath(string dirName = null)
        {
            StringBuilder dbPath = new StringBuilder();
            dbPath.Append(Environment.CurrentDirectory.Replace('\\', '/'));
            dbPath.Append("/data");
            //自定义二级文件夹
            if (!string.IsNullOrEmpty(dirName)) dbPath.Append($"/{dirName}");
            //检查目录是否存在，不存在则新建一个
            Directory.CreateDirectory(dbPath.ToString());
            dbPath.Append("/config.yaml");
            return dbPath.ToString();
        }

        /// <summary>
        /// 获取应用色图文件的绝对路径
        /// </summary>
        public static string GetHsoPath()
        {
            StringBuilder dbPath = new StringBuilder();
            dbPath.Append(Environment.CurrentDirectory.Replace('\\', '/'));
            dbPath.Append("/data/image/hso");
            //检查目录是否存在，不存在则新建一个
            Directory.CreateDirectory(dbPath.ToString());
            return dbPath.ToString();
        }

        /// <summary>
        /// 创建错误报告文件
        /// </summary>
        /// <param name="errorMessage">错误信息</param>
        public static void CrashLogGen(string errorMessage)
        {
            string fileName = $"{DateTime.Now.ToString(CultureInfo.CurrentCulture).Replace('/', '-').Replace(':', '-')}.log";
            using StreamWriter streamWriter = File.CreateText($@"{GetCrashLogPath()}/{fileName}");
            streamWriter.Write(errorMessage);
        }

        #endregion IO工具

        #region 文件读取工具

        /// <summary>
        /// 读取Json文件并返回为一个JObject
        /// </summary>
        /// <param name="jsonPath">json文件路径</param>
        /// <returns>保存整个文件信息的JObject</returns>
        public static JObject LoadJsonFile(string jsonPath)
        {
            try
            {
                StreamReader jsonFile = File.OpenText(jsonPath);
                JsonTextReader reader = new JsonTextReader(jsonFile);
                JObject jsonObject = (JObject)JToken.ReadFrom(reader);
                return jsonObject;
            }
            catch (Exception e)
            {
                ConsoleLog.Error("IO ERROR", $"读取文件{jsonPath}时出错，错误：\n{ConsoleLog.ErrorLogBuilder(e)}");
                return null;
            }
        }

        #endregion 文件读取工具

        #region 文件处理工具

        /// <summary>
        /// 解压程序，解压出的文件和原文件同路径
        /// </summary>
        /// <param name="LocalDBPath">数据文件路径</param>
        /// <param name="BinPath">二进制执行文件路径</param>
        public static void DecompressDBFile(string LocalDBPath, string BinPath)
        {
            string InputFile = LocalDBPath + "redive_cn.db.br";
            string outputFilePath = LocalDBPath;
            string outputFileName = "redive_cn.db";

            if (!File.Exists(outputFilePath + outputFileName))
            {
                try
                {
                    System.Diagnostics.Process.Start(BinPath, "-bd " + InputFile + " " + outputFilePath + " " + outputFileName);
                    //GC.Collect();
                }
                catch (Exception e)
                {
                    ConsoleLog.Error("BOSS信息数据库", $"BOSS信息数据库解压错误，请检查文件路径 错误:\n{e}");
                }
            }
        }

        #endregion 文件处理工具
    }
}