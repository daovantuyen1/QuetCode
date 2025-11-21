using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO
{
    public class C_SIGN_ROUTING
    {
        public string F_ROWID { set; get; }
        public string F_FLOW_ROW_ID { set; get; }
        public string F_SIGN_STATION_ID { set; get; }
        public string F_SIGN_STATION { set; get; }
        public string F_SIGN_EMP { set; get; }
        public string F_SIGN_FACTORY_ZONE { set; get; }
        public string F_CREATE_EMPNO { set; get; }
        public string F_CREATE_DATE { set; get; }
        public string F_SYSID { set; get; }
        public string F_SYSDATE { set; get; }
    }
    public class V_C_SIGN_ROUTING : C_SIGN_ROUTING
    {
        public int F_SIGN_SORT { set; get; } 
        
    }
}