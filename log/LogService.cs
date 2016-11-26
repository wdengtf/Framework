using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Net;
using log4net;
using System.Reflection;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.xml", Watch = true)] 
namespace Framework.log
{
    /// <summary>
    /// log4net操作日志类
    /// </summary>
    public class LogService
    {

        private static ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //创建日志记录组件实例 

        /// <summary>
        /// 功能：信息日志
        /// </summary>
        /// <param name="info"></param>
        public static void logInfo(string info)
        {
            log.Info(info);
        }

        /// <summary>
        /// 功能：异常日志
        /// </summary>
        /// <param name="ex"></param>
        public static void logDebug(string ex)
        {
            log.Debug(ex);
        }

        /// <summary>
        /// 功能：警告日志
        /// </summary>
        /// <param name="info"></param>
        public static void logWarn(string info)
        {
            log.Warn(info);
        }
        /// <summary>
        /// 功能：错误日志
        /// </summary>
        /// <param name="info"></param>
        public static void logError(string info)
        {
            log.Error(info);
        }

        /// <summary>
        /// 功能：严重错误日志
        /// </summary>
        /// <param name="info"></param>
        public static void logFatal(string info)
        {
            log.Fatal(info);
        }
    }
}
