using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{
    /// <summary>
    /// BANG LUU TRANG THAI HIEN TAI CUA DON
    /// </summary>
    [Table("R_APPLY_ORDER")]
    public class HI_R_APPLY_ORDER
    {
        public string F_APPLY_NO { set; get; }
        public string F_APPLY_TYPE { set; get; }
        public string F_APPLY_EMP { set; get; }
        public string F_APPLY_NAME { set; get; }
        public string F_APPLY_TIME { set; get; }
        public string F_STATUS { set; get; }
        public string F_SIGN_STATION_NAME { set; get; }
        public string F_SIGN_STATION_NO { set; get; }
        public string F_SIGN_EMP { set; get; }
        public string F_FLOW_NAME { set; get; }
        public string F_FLOW_ROW_ID { set; get; }
        public string F_DATA2 { set; get; }
        public string F_DATA3 { set; get; }
        public string F_SIGN_AGENT_EMP { set; get; }
        public string F_SIGN_AGENT_NAME { set; get; }
        public string F_SIGN_AGENT_MAIL { set; get; }
        public string F_CREATE_EMP { set; get; }
        public string F_CREATE_TIME { set; get; }
        public string F_UPDATE_EMP { set; get; }
        public string F_UPDATE_TIME { set; get; }
    }
}