using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{

    /// <summary>
    /// BANG LUU LICH SU KY DUYET
    /// </summary>
    public class HI_R_SIGN_DETAIL
    {

        public string F_ROW_ID { set; get; }
        public string F_APPLY_NO { set; get; }
        public string F_FLOW_NAME { set; get; }
        public string F_FLOW_ROW_ID { set; get; }
        public string F_STATION_NAME { set; get; }
        public int? F_SIGN_SORT { set; get; }
        public int? F_RETURN_SORT { set; get; }
        public string F_SIGN_EMP { set; get; }
        public string F_SIGN_NAME { set; get; }
        public string F_SIGN_MAIL { set; get; }
        public string F_POSITION { set; get; }
        public string F_CONFIG_FLAG { set; get; }
        public string F_MAIL_FLAG { set; get; }
        public string F_EMP_LEVEL { set; get; }
        public int? F_EMP_LEVEL_OF_SORT { set; get; }
        public string F_NODE_INPUT_EMP { set; get; }
        public string F_NODE_UP_FILE { set; get; }
        public string F_NODE_REMARK { set; get; }
        public string F_CREATE_EMP { set; get; }
        public string F_CREATE_TIME { set; get; }
        public string F_SIGN_STATUS { set; get; }
        
    }

}