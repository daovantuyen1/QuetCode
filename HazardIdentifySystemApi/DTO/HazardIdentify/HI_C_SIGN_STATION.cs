using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{
    /// <summary>
    /// BANG LUU CAU HINH LUU TRINH KY
    /// </summary>
    [Table("C_SIGN_STATION")]
    public class HI_C_SIGN_STATION
    {

        public string F_ROW_ID { set; get; }
        public string F_FLOW_ROW_ID { set; get; }
        public string F_SIGN_STATION { set; get; }
        public int? F_SIGN_SORT { set; get; }
        public int? F_RETURN_STATION { set; get; }
        public string F_CONFIG_FLAG { set; get; }
        public string F_MAIL_FLAG { set; get; }
        public string F_EMP_LEVEL { set; get; }
        public int? F_EMP_LEVEL_OF_SORT { set; get; }
        public string F_NODE_INPUT_EMP { set; get; }
        public string F_NODE_UP_FILE { set; get; }
        public string F_NODE_REMARK { set; get; }
        public string F_CREATE_EMP { set; get; }
        public string F_CREATE_TIME { set; get; }
        public string F_UPDATE_EMP { set; get; }
        public string F_UPDATE_TIME { set; get; }
    }
}