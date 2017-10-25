using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using log4net.Appender;

namespace NB_Web.Common
{
    public class LogUtility
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// 记录正常日志
        /// </summary>
        /// <param name="strInfo"></param>
        public static void WriteInfo(string strInfo)
        {
            log.Info(strInfo);
            Console.WriteLine(strInfo);
        }

        /// <summary>
        /// 记录调试日志
        /// </summary>
        /// <param name="strInfo"></param>
        public static void WriteDebugInfo(string strInfo)
        {
            log.Debug(strInfo);
            Console.WriteLine(strInfo);
        }

        /// <summary>
        /// 打印错误信息
        /// </summary>
        /// <param name="strInfo"></param>
        public static void WriteError(string strInfo)
        {
            log.Error(strInfo);
            Console.WriteLine(strInfo);
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="st">出现错误的堆栈</param>
        /// <param name="ex">错误对象</param>
        public static void WriteError(StackTrace st, Exception ex)
        {
            try
            {
                StackFrame sf = st.GetFrame(0);
                string info = string.Format("Err @【{0}】 错误信息：【{1}】", sf.GetMethod().Name, ex.Message) + Environment.NewLine + ex.StackTrace.ToString();
                log.Error(info);
                Console.WriteLine(info);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
        }

        public static void WriteWarn(string strInfo)
        {
            log.Warn(strInfo);
            Console.WriteLine(strInfo);
        }

        /// <summary>
        /// 设置日志存写路径
        /// </summary>
        /// <param name="logFolderName"></param>
        /// <param name="fileName"></param>
        public static void SetLogPath(string productName)
        {
            //log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(string.Format("{0}.log.config", productName)));
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData, Environment.SpecialFolderOption.Create);
            string logPath = Path.Combine(localPath, "Allegion\\Logs");
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            var repository = log.Logger.Repository;
            var appenders = repository.GetAppenders();
            var targetApder = appenders.First(p => p.Name == "LogFileAppender") as RollingFileAppender;
            targetApder.File = string.Format("{0}\\{1}.log", logPath, productName);
            targetApder.ActivateOptions();
        }
    }
}
