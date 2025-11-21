using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.Commons.HazardIdentifySystem
{
    public class HIDbHelper
    {
        #region SingelTon
        private static object lockObj = new object();
        private HIDbHelper() { }
        private static HIDbHelper _instance;
        public static HIDbHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new HIDbHelper();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion

       public SqlConnection GetDBCnn()
        {
            return new SqlConnection(Constant.SQL_SERVER_DB_SECURITY);
        }
    }
}