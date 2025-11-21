using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{
    /// <summary>
    /// BANG LUU TEN  LUU TRINH KY
    /// </summary>
    [Table("C_SIGN_FLOW")]
    public class HI_C_SIGN_FLOW
    {
        public string F_ROW_ID { set; get; }
        public string F_FLOW_NAME { set; get; }
        public string F_FLOW_DESC { set; get; }
        public string F_STATUS { set; get; }
        public string F_CREATE_EMP { set; get; }
        public string F_CREATE_TIME { set; get; }
        public string F_UPDATE_EMP { set; get; }
        public string F_UPDATE_TIME { set; get; }
    }
}