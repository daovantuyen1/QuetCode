using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Commons.HazardIdentifySystem
{
    public enum LogType
    {
        Info,
        Debug,
        Fatal,
        Error,
        Warn
    }
    public class HILog4netLogging
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void SaveLog(LogType logType, string mess, params object[] param)
        {

            //1.Save log to db or any external log service other
            //new Task(() =>
            //{
            //    string log = param.Count() > 0 ? string.Format(mess, param) : mess;
            //    string fullLog = $"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}[{logType.ToString().ToUpper()}] - {log}";

            //}).Start();

            // 2. Save log to local file
            switch (logType)
            {
                case LogType.Info: if (param.Count() > 0) _log.InfoFormat(mess, param); else _log.Info(mess); break;
                case LogType.Debug: if (param.Count() > 0) _log.DebugFormat(mess, param); else _log.Debug(mess); break;
                case LogType.Fatal: if (param.Count() > 0) _log.FatalFormat(mess, param); else _log.Fatal(mess); break;
                case LogType.Error: if (param.Count() > 0) _log.ErrorFormat(mess, param); else _log.Error(mess); break;
                case LogType.Warn: if (param.Count() > 0) _log.WarnFormat(mess, param); else _log.Warn(mess); break;
                default: break;
            }
        }
    }

}