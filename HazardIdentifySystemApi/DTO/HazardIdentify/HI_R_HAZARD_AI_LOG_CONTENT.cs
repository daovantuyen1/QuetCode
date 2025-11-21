using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{
    [Table("R_HAZARD_AI_LOG_CONTENT")]
    public class HI_R_HAZARD_AI_LOG_CONTENT
    {
        public string F_ROW_ID { set; get; }
        public string F_DOCNO { set; get; }
        public string F_CONTENT { set; get; }
        public string F_TYPE { set; get; }
        public string F_SORT { set; get; }
        public string F_IS_USERADD { set; get; }
        public string F_CREATE_EMP { set; get; }
        public string F_CREATE_TIME { set; get; }
    }
}