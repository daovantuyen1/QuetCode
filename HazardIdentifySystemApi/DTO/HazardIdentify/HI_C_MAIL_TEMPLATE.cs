using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO.HazardIdentify
{
    public class HI_C_MAIL_TEMPLATE
    {
        public string ROW_ID { set; get; }
        public string TEMPLATE_NAME { set; get; }
        public string MAIL_SUBJECT { set; get; }
        public string MAIL_CONTENT { set; get; }
        public string DATA1 { set; get; }
        public string DATA2 { set; get; }
        public string DATA3 { set; get; }
        public string CREATE_EMP { set; get; }
        public string CREATE_TIME { set; get; }
        public string UPDATE_EMP { set; get; }
        public string UPDATE_TIME { set; get; }

    }
}