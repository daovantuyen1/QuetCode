using Dapper;
using HazardIdentifySystemApi.Businesses.HazardIdentifySystem;
using HazardIdentifySystemApi.DTO.HazardIdentify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Commons.HazardIdentifySystem
{

    public class HILogging
    {

        #region SingelTon
        private static object lockObj = new object();
        private HILogging() { }
        private static HILogging _instance;
        public static HILogging Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HILogging();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

        public void SaveLog(string message)
        {

            if (!string.IsNullOrWhiteSpace(message))
            {
                using (var cnn = HIDbHelper.Instance.GetDBCnn())
                {
                    try
                    {
                        var acc = HIAccountBusiness.Instance.GetAccountFromSession();
                        string IP = Util.GetIp();
                        cnn.Open();
                        cnn.Execute("INSERT INTO TB_LOG_HISTORY VALUES(@F_ACCOUNT,@F_IP,@F_DETAIL,GETDATE())",
                            new
                            {
                                F_ACCOUNT = acc?.F_EMPNO,
                                F_IP = IP,
                                F_DETAIL = message?.Trim()
                            });
                        cnn.Execute("  delete from  TB_LOG_HISTORY  WHERE   CREATE_TIME<= getdate() -180 ");

                        HILog4netLogging.SaveLog(LogType.Info, $"F_ACCOUNT:{acc?.F_EMPNO},F_IP:{IP},F_DETAIL:[{message?.Trim()}]");

                    }
                    catch (Exception ex)
                    {

                    }
                }

            }

        }
    }

}