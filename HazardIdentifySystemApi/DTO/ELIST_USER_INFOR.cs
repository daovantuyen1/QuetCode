using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HazardIdentifySystemApi.DTO
{
    public class ELIST_USER_INFOR
    {

        public string USER_ID { set; get; }
        public string USER_NAME { set; get; }
        public string USER_NAME_EXT { set; get; }
        public string SEX { set; get; }
        public string GRADE { set; get; }
        public string JOB_TITLE { set; get; }
        public string CURRENT_OU_CODE { set; get; }
        public string CURRENT_OU_NAME { set; get; }
        public string UPPER_OU_CODE { set; get; }
        public string NOTES_ID { set; get; }
        public string CL_ID { set; get; }
        public string CL_NAME { set; get; }
        public string EMAIL { set; get; }
        public string LOCATION { set; get; }
        public string ALL_MANAGERS { set; get; }
        public string SITE_ALL_MANAGERS { set; get; }
        public string BU_ALL_MANAGERS { set; get; }
        public string CARD_ID { set; get; }
        public string USER_LEVEL { set; get; }
        public string HIREDATE { set; get; }
        public string LEAVEDAY { set; get; }
        public string JOB_TYPE { set; get; }
        public string UPPER_OU_NAME { set; get; }
        public string NOTDUTY { set; get; }
        public string TRAVEL { set; get; }
        public string USER_ID_EXT { set; get; }
        public string ASSISTANT_ID { set; get; }
        public string USER_BU { set; get; }
        public string USER_NAME1 { set; get; }
        public string USER_IDCARD { set; get; }
    }

    public class AGENT_INFOR
    {
        public string AGENT_LINE { set; get; }
        public string AGENT_OU { set; get; }
        public string AGENT_ORDER { set; get; }
        public string AGENT_WHO { set; get; }
        public string NOTDUTY { set; get; }
        public string TRAVEL { set; get; }
    }
}