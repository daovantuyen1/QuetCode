using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO
{
    public class R_SIGN_LINKED
    {

        public string F_ROW_ID { set; get; }
        public string APPLY_NO { set; get; }
        public string F_FLOW_ROW_ID { set; get; }
        public string STATION_NAME { set; get; }
        public int SIGN_SORT { set; get; }
        public string SIGN_EMP { set; get; }
        public string SIGN_NAME { set; get; }
        public string SIGN_MAIL { set; get; }
        public int RETURN_SORT { set; get; }
        public string CREATE_EMP { set; get; }
        public string CREATE_DATE { set; get; }
    }
}