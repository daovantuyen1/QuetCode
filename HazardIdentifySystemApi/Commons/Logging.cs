using HazardIdentifySystemApi.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using HazardIdentifySystemApi.Commons.HazardIdentifySystem;

namespace HazardIdentifySystemApi.Commons
{

    public class Logging
    {

        #region SingelTon
        private static object lockObj = new object();
        private Logging() { }
        private static Logging _instance;
        public static Logging Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new Logging();
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
                        if (HttpContext.Current.Session != null && HttpContext.Current.Session["Account"] != null)
                        {
                            var acc = HttpContext.Current.Session["Account"] as TB_ACCOUNT;
                            cnn.Open();
                            cnn.Execute("INSERT INTO TB_LOG_HISTORY VALUES(@F_ACCOUNT,@F_IP,@F_DETAIL,GETDATE())",
                                new
                                {
                                    F_ACCOUNT = acc?.F_EMPNO,
                                    F_IP = Util.GetIp(),
                                    F_DETAIL = message?.Trim()
                                });
                            cnn.Execute("  delete from  TB_LOG_HISTORY  WHERE   F_SYSDATE<= getdate() -180 ");

                        }

                    }
                    catch
                    {

                    }
                }

            }

        }
    }

}