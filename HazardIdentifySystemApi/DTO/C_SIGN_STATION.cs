using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO
{
    public class C_SIGN_STATION
    {
        public string F_ROW_ID { set; get; }
        public string F_FLOW_ROW_ID { set; get; }
        public string F_SIGN_STATION { set; get; }
        public int F_SIGN_SORT { set; get; }
        public int F_RETURN_STATION { set; get; }
        public string F_CREATE_EMPNO { set; get; }
        public string F_CREATE_DATE { set; get; }
        public string FIXED_SIGN_EMP { set; get; }
        /// <summary>
        /// -- level cua nguoi ky tai node nay 
        /// </summary>
        public string USER_LEVEL { set; get; }

        /// <summary>
        /// nguoi ky tai node nay la chu quan cua sign_sort nao
        /// </summary>
        public int? MANAGE_OF_SORT { set; get; }

        /// <summary>
        /// cho phep dk sua doi ma the cua node ky
        /// </summary>
        public string F_ALLOW_CHANGE { set; get; }
        

    }
    public class V_C_SIGN_STATION: C_SIGN_STATION
    {
        public string F_FLOW_NAME { set; get; }
        public string F_SIGN_EMP { set; get; }
        public string F_SIGN_NAME { set; get; }
        public string F_SIGN_MAIL { set; get; }
    }
}